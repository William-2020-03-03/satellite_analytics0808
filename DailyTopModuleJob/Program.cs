using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using System.Text.Json;


namespace DailyTopModuleJob
{



    class Program
    {
        static void Main(string[] args)
        {
            string sqlConnstr = "Server=localhost;Database=lr_satellite;User Id=satellite_user;Password=Common@123;TrustServerCertificate=True;";
            string redisConnStr = "localhost:6379";

            using var redis = ConnectionMultiplexer.Connect(redisConnStr);
            var db = redis.GetDatabase();

            var results = new List<object>();


            try
            {
                using (var conn = new SqlConnection(sqlConnstr))
                {
                    conn.Open();
                    Console.WriteLine("Connection successful!");

                    string sql = @"select top 3 count(*) as mycount, module, operation from [dbo].[user_operation_log] group by module, operation order by mycount desc;";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(new
                                {
                                    Count = reader.GetInt32(0),
                                    Module = reader.GetString(1),
                                    Operation = reader.GetString(2)
                                });
                            }
                        }
                    }
                }

                string json = JsonSerializer.Serialize(results);
                string key = $"top3moduleoperation:summary:{DateTime.UtcNow:yyyyMMdd}";

                db.StringSet(key, json, TimeSpan.FromHours(24));

                Console.WriteLine($"Ok, write into Redis Key={key}");

                //Console.WriteLine($"Key={key}, Value={db.StringGet(key)}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Sql Error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error : {ex.Message}");
            }
        }
    }



}

