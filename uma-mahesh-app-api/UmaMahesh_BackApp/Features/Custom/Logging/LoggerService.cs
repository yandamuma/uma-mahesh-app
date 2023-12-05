using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace UmaMahesh_BackApp.Features.Custom.Logging;

public static class LoggerServiceLocal
{
    //public static void WithSimpleConfiguration(this LoggerConfiguration loggerConfig, IServiceProvider provider,
    //                                                                                string? applicationName, IConfiguration config)
    //{
    //    AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
    //    string? value = config.GetSection("SeriLog:ConnectionStringName").Value;
    //    if (string.IsNullOrEmpty(value))
    //    {
    //        throw new Exception("SeriLog:ConnectionStringName value is missing ");
    //    }
    //    string? connectionString = config.GetConnectionString(value);
    //    if (string.IsNullOrEmpty(connectionString))
    //    {
    //        throw new Exception("Could not get the Connection String. Please enter the Connection String with the name " + connectionString + " ");
    //    }

    //    loggerConfig.ReadFrom.Configuration(config).Enrich.FromLogContext().Enrich.WithMachineName().Enrich.WithProcessId()
    //        .Enrich.WithProcessName().Enrich.WithThreadId().Enrich.WithProperty("Assembly", assemblyName.Name ?? "")
    //        .Enrich.WithProperty("Version", $"{assemblyName.Version}").WriteTo.Logger(delegate (LoggerConfiguration lc)
    //        {
    //            lc.Filter.ByExcluding(Matching.WithProperty("ElapsedMilliseconds")).Filter.ByExcluding(Matching.WithProperty("UsageName"))
    //            .WriteTo.MSSqlServer(connectionString,"SeriLog", null, LogEventLevel.Verbose, 50, null, null, autoCreateSqlTable: true, GetLogSqlColumnOptions());
    //        });
    //}



    public static void WithSinkConfiguration(LoggerConfiguration loggerConfig, IServiceProvider provider,
                                                                                    string? applicationName, IConfiguration config)
    {
        if (loggerConfig == null)
        {
            throw new Exception("LoggerConfiguration is null");
        }

        AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
        string? value = config.GetSection("SeriLog:ConnectionStringName").Value;
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("SeriLog:ConnectionStringName value is missing ");
        }
        string? connectionString = config.GetConnectionString(value);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Could not get the Connection String. Please enter the Connection String with the name " + connectionString + " ");
        }

        var sinkOptions = new MSSqlServerSinkOptions()
        {
            TableName = "SeriLog",
            AutoCreateSqlTable = true,

        };

        loggerConfig.ReadFrom.Configuration(config).Enrich.FromLogContext().Enrich.WithMachineName().Enrich.WithProcessId()
            .Enrich.WithProcessName().Enrich.WithThreadId().Enrich.WithProperty("Assembly", assemblyName.Name ?? "")
            .Enrich.WithProperty("Version", $"{assemblyName.Version}").WriteTo.Logger(delegate (LoggerConfiguration lc)
            {
                lc.Filter.ByExcluding(Matching.WithProperty("ElapsedMilliseconds")).Filter.ByExcluding(Matching.WithProperty("UsageName"))
                .WriteTo.MSSqlServer(connectionString, sinkOptions, null, null, LogEventLevel.Verbose, null, GetLogSqlColumnOptions());
            });
    }

    private static ColumnOptions GetLogSqlColumnOptions()
    {
        ColumnOptions columnOptions = new ColumnOptions();
        columnOptions.Store.Remove(StandardColumn.Properties);
        columnOptions.Store.Add(StandardColumn.LogEvent);
        columnOptions.LogEvent.ExcludeStandardColumns = true;
        columnOptions.LogEvent.ExcludeAdditionalProperties = true;
        columnOptions.AdditionalColumns = new Collection<SqlColumn>
        {
            new SqlColumn
            {
                ColumnName = "ActionName",
                AllowNull = true,
                DataType = SqlDbType.VarChar,
                DataLength = 1000
            },
            new SqlColumn
            {
                ColumnName = "Assembly",
                AllowNull = true,
                DataType = SqlDbType.VarChar,
                DataLength = 250
            },
            new SqlColumn
            {
                ColumnName = "ProcessId",
                AllowNull = true,
                DataType = SqlDbType.Int,

            },
            new SqlColumn
            {
                ColumnName = "ThreadId",
                AllowNull = true,
                DataType = SqlDbType.Int,
            },
            new SqlColumn
            {
                ColumnName = "MachineName",
                AllowNull = true,
                DataType = SqlDbType.VarChar,
                DataLength = 100
            }
        };
        return columnOptions;
    }
}

