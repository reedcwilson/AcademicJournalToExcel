using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectModel.Tests
{
	[TestClass]
	public class MungerTests
	{
		#region Text
		private const string TEXT = @"How many adolescents start smoking each day in the United States?
Gilpin, Elizabeth A; Choi, Won S; Berry, Charles; Pierce, John P
Journal of Adolescent Health, 1999, Vol.25(4), pp.248-255, 1999
Available Online
Journal of Adolescent Health

    Description

    Purpose: To provide daily estimates of first experimentation and attainment of established smoking in adolescents 11–17 years of age. Methods: The 1989 and 1993 Teenage Attitudes and Practices Surveys (TAPS) (16,954 observations) was used to estimate rates in 1991 for: (a) first experimentation (even a few puffs), and (b) established smoking (reaching a lifetime level of at least 100 cigarettes). The 1992 and 1993 Current Population Surveys (CPS) (82,279 adults) allowed calculation of year-specific initiation rates (1980–1989) from the age respondents reported as having started smoking “fairly regularly.” Rates were applied to Census data for the appropriate years to yield numbers accruing each day. Estimates were calculated for adolescents (11- to 17-year-olds) and youth (11- to 20-year-olds). Results: For TAPS, in 1991, 4824 adolescents first tried cigarettes and 1975 became established smokers each day. Considering all youth, these estimates increase to 5497/day for first experimentation and to 2933/day for established smokers. For CPS, from 1980 to 1989, around 2300 adolescents initiated fairly regular smoking each day. For all youth, the estimate has increased to about 3400/day. Conclusions: Because approximately 4800 adolescents and 5500 youth appear to be experimenting with cigarettes for the first time each day, and close to 3000 youth become established smokers daily, increased prevention efforts are clearly justified.
    Subjects
        Smoking uptake
        Smoking initiation
        Adolescent smoking
    Identifier

    ISSN: 1054-139X (sciversesciencedirect_elsevierS1054-139X(99)00024-5)
    Language

    English
    Creation Date

    1999";

		private const string ALTTEXT = @"Pseudocyesis in an adolescent using the long-acting contraceptive Depo-Provera
Flanagan, Patricia J; Harel, Zeev
Journal of Adolescent Health, 1999, Vol.25(3), pp.238-240, 1999
Available Online
Journal of Adolescent Health

    Subjects
        Pseudocyesis
        Adolescents
        Medroxyprogesterone
        Depo-Provera
    Identifier

    ISSN: 1054-139X (sciversesciencedirect_elsevierS1054-139X(99)00021-X)
    Language

    English
    Creation Date

    1999

";
		#endregion

		private Munger _munger;

		[TestInitialize]
		public void Initialize()
		{
			_munger = new Munger();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_munger = null;
		}

		[TestMethod]
		public void MungeTest()
		{
			_munger.Munge(TEXT);
			var str = _munger.GetCopy();
			Assert.IsTrue(str != null);
		}
	}
}
