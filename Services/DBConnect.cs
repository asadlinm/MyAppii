using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RabiteBankAPI.Services
{
	public class DBConnect
	{
		private static string connectionString;
		static DBConnect()
		{
			connectionString = "Server=DESKTOP-KESMHLM;Database=RabiteBank;";
		}
		public static bool insertLog(string value)
		{
			bool isSuccess = false;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					SqlCommand cmd = new SqlCommand("InsertLog", connection);
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@value", SqlDbType.NVarChar).Value = value;

					cmd.Parameters["@value"].Direction = ParameterDirection.Input;

					cmd.ExecuteNonQuery();

					isSuccess = true;
				}
				catch (Exception ex)
				{
					
				}
				finally
				{
					connection.Close();
				}

				return isSuccess;
			}
		}
	}
}
