using System;
using System.Text.RegularExpressions;
using System.Linq;


namespace NiceScreen
{
	public class MathsParser
	{
		private string _input;

		public MathsParser(string InputString)
		{

			_input = InputString;
		}

		public double Parse()
		{
			var parenthetisedSectionsResolved = ResolveParentheses (_input);
			return calculateSection(parenthetisedSectionsResolved);
		}

		private double calculateSection(string section)
		{
			var digits = Regex.Split (section, @"\D+").ToList ();
			var operators = Regex.Matches (section, @"[a-d]").OfType<Match>().Select(m => m.Groups[0].Value).ToList();

			var result = 0.0;

			operators.ForEach (o => { 
				result = Calculate(o, double.Parse(digits[0]), double.Parse(digits[1]));
				digits.RemoveRange(0,2);
				digits.Insert(0, result.ToString());
			});

			return result;
		}

		private double Calculate(string operation, double value1, double value2) 
		{
			switch (operation) {
			case "a": 
				return value1 + value2;
			case "b" :
				return value1 - value2;
			case "c":
				return value1 * value2;
			case "d":
				return value1 / value2;
			}

			throw new ArgumentException(String.Format("Operation {0} not supported", operation));
		}

		private string ResolveParentheses(string input)
		{
			var outputString = input;

			if (!ContainsGroupedSection(input))
				return outputString;

			var startBracketIndex = input.IndexOf ("e");
			var nestedBracketCount = 0;
			var currentCharIndex = 0;
			var closingBracketIndex = 0;
			var firstChunk = GetFirstSection (input, startBracketIndex, ref nestedBracketCount, ref currentCharIndex, ref closingBracketIndex);

			if (ContainsGroupedSection(firstChunk)) {
				firstChunk = ResolveParentheses (firstChunk);
			}
			var sectionResults = calculateSection(firstChunk);

			outputString = input.Substring (0, startBracketIndex) + sectionResults.ToString () 
				+ input.Substring((closingBracketIndex + startBracketIndex + 2), input.Length - (closingBracketIndex +1 + startBracketIndex )  -1);

			return ResolveParentheses(outputString);

		}

		static bool ContainsGroupedSection (string firstChuck)
		{
			return firstChuck.IndexOf ("e") != -1;
		}

		static string GetFirstSection (string input, int startBracketIndex, ref int nestedBracketCount, ref int currentCharIndex, ref int closingBracketIndex)
		{
			foreach (char c in input.Substring (startBracketIndex + 1)) {
				if (c == 'e') {
					nestedBracketCount += 1;
				}
				if (c == 'f') {
					if (nestedBracketCount == 0) {
						closingBracketIndex = currentCharIndex;
						break;
					}
					else {
						nestedBracketCount -= 1;
					}
				}
				currentCharIndex += 1;
			}
			var firstChuck = input.Substring (startBracketIndex + 1, closingBracketIndex);
			return firstChuck;
		}
	}
}

