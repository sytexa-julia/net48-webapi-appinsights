using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class CityPopulation
    {
        public string Name { get;set;}
        public int Population { get;set;}
    }
    public class ValuesController : ApiController
    {
        private static readonly TelemetryClient telemetryClient = new TelemetryClient();

        // GET api/values
        public async Task<IEnumerable<CityPopulation>> GetAsync()
        {
            var result = new List<CityPopulation>();
            var connString = "Host=localhost;Port=5432;Username=world;Password=world123;Database=world-db";
            var sql = "select name, population from city where country_code = 'USA'";

            using (var operation = telemetryClient.StartOperation<DependencyTelemetry>("Query: city"))
            {
                var stopwatch = Stopwatch.StartNew();
                var success = false;

                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        await conn.OpenAsync();

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string name = reader.GetString(0);
                                int pop = reader.GetInt32(1);
                                result.Add(new CityPopulation { Name = name, Population = pop });
                            }
                        }
                    }

                    success = true;
                }
                catch (Exception ex)
                {
                    telemetryClient.TrackException(ex);
                    throw;
                }
                finally
                {
                    stopwatch.Stop();
                    var dependency = operation.Telemetry;
                    dependency.Type = "PostgreSQL";
                    dependency.Target = "world-db";
                    dependency.Data = sql;
                    dependency.Duration = stopwatch.Elapsed;
                    dependency.Success = success;
                    dependency.ResultCode = success ? "OK" : "ERROR";
                }
            }


            return result;

        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
