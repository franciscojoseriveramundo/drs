using DRS.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace DRS.PostgreSQL
{

    public class DbContextExtensions
    {
        DRSContext dbDRS = new DRSContext();

        public DataTable SqlQueryAsync(string sql, List<NpgsqlParameter> parameters)
        {
            DataTable dt = new DataTable();

            using (var command = dbDRS.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach(var item in parameters)
                {
                    command.Parameters.Add(item);
                    
                }

                dbDRS.Database.OpenConnection();

                var result = command.ExecuteReader();

                dt.Load(result);

                dbDRS.Database.CloseConnection();
            }

            return dt;
        }

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}
