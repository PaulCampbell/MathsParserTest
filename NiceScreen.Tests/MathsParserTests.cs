using System;
using NUnit.Framework;
using Shouldly;

namespace NiceScreen.Tests
{
	[TestFixture]
	public class MathsParserTests
	{
		[TestCase("3a2", 5)]
		[TestCase("3a7",10)]
		public void CanAdd(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("3a2a5", 10)]
		public void CanAddMulitple(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("3b2", 1)]
		[TestCase("3b7",-4)]
		[TestCase("15b7b1",7)]
		public void CanSubtract(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("3c2", 6)]
		[TestCase("3c7",21)]
		[TestCase("15c7c2",210)]
		public void CanMulitply(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("3d2", 1.5)]
		[TestCase("21d7",3)]
		[TestCase("14d7d2",1)]
		public void CanDivide(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("1ae4c2f", 9)]
		[TestCase("1ae4c2fa6", 15)]
		[TestCase("1ae4c2fb6", 3)]
		public void BracketsTakePrecedence(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("e4a1fae5c2f", 15)]
		[TestCase("1ae4c2fae5c2f", 19)]
		public void InlineBrackets(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}

		[TestCase("e4a1ae2a2ffae5c2f", 19)] // (4+1+(2+2))+(5*2)
		[TestCase("2aee2a4c41fc4f", 986)] // 2+((2+4*41)*4)
		public void NestedBrackets(string inputString, double result)
		{
			//(4+1+(2+2))+(5*2)
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}
	}
}

