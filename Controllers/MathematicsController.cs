using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabiteBankAPI.Models;
using RabiteBankAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace RabiteBankAPI.Controllers
{
	[ApiController]
	public class MathematicsController : ControllerBase
	{
		Calculator.CalculatorSoapClient client;
		Calculator.CalculatorSoapClient Client { get { return client ?? (client = new Calculator.CalculatorSoapClient(new BasicHttpBinding
		{
			SendTimeout = new TimeSpan(0, 1, 0),
			MaxBufferSize = int.MaxValue,
			MaxReceivedMessageSize = int.MaxValue
		},
				new EndpointAddress(@"http://www.dneonline.com/calculator.asmx"))); } }

		[HttpPost("Add")]
		public int Add([FromBody] Numbers numbers)
		{
			DBConnect.insertLog("Request to JSON");
			var task = Task.Run(() => Client.AddAsync(numbers.intA, numbers.intB));
			task.Wait();
			
			DBConnect.insertLog("Response from SOAP: " + task.Result);
			return task.Result;
		}

		[HttpPost("Divide")]
		public int Divide([FromBody] Numbers numbers)
		{
			DBConnect.insertLog("Request to JSON");
			var task = Task.Run(() => Client.DivideAsync(numbers.intA, numbers.intB));
			task.Wait();
			DBConnect.insertLog("Response from SOAP: " + task.Result);
			return task.Result;
		}

		[HttpPost("Subtract")]
		public int Subtract([FromBody] Numbers numbers)
		{
			DBConnect.insertLog("Request to JSON");
			var task = Task.Run(() => Client.SubtractAsync(numbers.intA, numbers.intB));
			task.Wait();
			DBConnect.insertLog("Response from SOAP: " + task.Result);
			return task.Result;
		}

		[HttpPost("Multiply")]
		public int Multiply([FromBody] Numbers numbers)
		{
			DBConnect.insertLog("Request to JSON");
			var task = Task.Run(() => Client.MultiplyAsync(numbers.intA, numbers.intB));
			task.Wait();
			DBConnect.insertLog("Response from SOAP: " + task.Result);
			return task.Result;
		}
	}
}
