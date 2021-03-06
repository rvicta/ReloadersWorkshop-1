﻿//============================================================================*
// cListViewEvaluationComparer.cs
//
// Copyright © 2013-2017, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.Windows.Forms;

using ReloadersWorkShop.Preferences;

//============================================================================*
// CommonLib Using Statements
//============================================================================*

using CommonLib.Conversions;

//============================================================================*
// Namespace
//============================================================================*

namespace ReloadersWorkShop
	{
	//============================================================================*
	// cListViewEvaluationComparer Class
	//============================================================================*

	class cListViewEvaluationComparer : cListViewComparer
		{
		//============================================================================*
		// Private Data Members
		//============================================================================*

		private cDataFiles m_DataFiles = null;

		//============================================================================*
		// cListViewEvaluationComparer() - Constructor
		//============================================================================*

		public cListViewEvaluationComparer(cDataFiles DataFiles, int nSortColumn, SortOrder SortOrder)
			: base(nSortColumn, SortOrder)
			{
			m_DataFiles = DataFiles;
			}

		//============================================================================*
		// Compare()
		//============================================================================*

		public override int Compare(Object Object1, Object Object2)
			{
			if (Object1 == null)
				{
				if (Object2 == null)
					return (0);
				else
					return (-1);
				}
			else
				{
				if (Object2 == null)
					return (1);
				}

			cEvaluationItem Item1 = (cEvaluationItem) (Object1 as ListViewItem).Tag;
			cEvaluationItem Item2 = (cEvaluationItem) (Object2 as ListViewItem).Tag;

			if (Item1 == null)
				{
				if (Item2 == null)
					return (0);
				else
					return (0);
				}
			else
				{
				if (Item2 == null)
					return (1);
				}

			//----------------------------------------------------------------------------*
			// Do Special Compares
			//----------------------------------------------------------------------------*

			bool fSpecial = false;
			int rc = 0;

			string strPart1 = "";
			string strPart2 = "";

			cBatch Batch1 = null;
			cBatch Batch2 = null;

			switch (SortColumn)
				{
				//----------------------------------------------------------------------------*
				// Batch ID
				//----------------------------------------------------------------------------*

				case 0:
					rc = Item1.ChargeTest.BatchID.CompareTo(Item2.ChargeTest.BatchID);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Caliber
				//----------------------------------------------------------------------------*

				case 1:
					rc = Item1.Load.Caliber.CompareTo(Item2.Load.Caliber);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Bullet
				//----------------------------------------------------------------------------*

				case 2:
					rc = Item1.Load.Bullet.Manufacturer.CompareTo(Item2.Load.Bullet.Manufacturer);

					if (rc == 0)
						{
						strPart1 = Item1.Load.Bullet.PartNumber;
						strPart2 = Item2.Load.Bullet.PartNumber;

						if (strPart1.Length != strPart2.Length)
							{
							if (strPart1.Length > strPart2.Length)
								{
								string strPad = "";

								while (strPart1.Length > strPart2.Length + strPad.Length)
									strPad += " ";

								strPart2 = strPad + strPart2;
								}
							else
								{
								string strPad = "";

								while (strPart2.Length > strPart1.Length + strPad.Length)
									strPad += " ";

								strPart1 = strPad + strPart1;
								}
							}


						rc = strPart1.CompareTo(strPart2);

						if (rc == 0)
							{
							rc = Item1.Load.Powder.CompareTo(Item2.Load.Powder);
							}
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Powder
				//----------------------------------------------------------------------------*

				case 3:
					rc = Item1.Load.Powder.Manufacturer.CompareTo(Item2.Load.Powder.Manufacturer);

					if (rc == 0)
						{
						strPart1 = Item1.Load.Powder.Type;
						strPart2 = Item2.Load.Powder.Type;

						if (strPart1.Length != strPart2.Length)
							{
							string strPad = "";

							if (strPart1.Length < strPart2.Length)
								{
								while (strPart1.Length + strPad.Length < strPart2.Length)
									strPad += " ";

								strPart1 = strPad + strPart1;
								}
							else
								{
								while (strPart2.Length + strPad.Length < strPart1.Length)
									strPad += " ";

								strPart2 = strPad + strPart2;
								}
							}

						rc = strPart1.CompareTo(strPart2);

						if (rc == 0)
							rc = Item1.Charge.PowderWeight.CompareTo(Item2.Charge.PowderWeight);
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Charge
				//----------------------------------------------------------------------------*

				case 4:
					rc = Item1.Charge.PowderWeight.CompareTo(Item2.Charge.PowderWeight);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Primer
				//----------------------------------------------------------------------------*

				case 5:
					rc = Item1.Load.Primer.Manufacturer.CompareTo(Item2.Load.Primer.Manufacturer);

					if (rc == 0)
						{
						strPart1 = Item1.Load.Primer.Model;
						strPart2 = Item2.Load.Primer.Model;

						if (strPart1.Length != strPart2.Length)
							{
							string strPad = "";

							if (strPart1.Length < strPart2.Length)
								{
								while (strPart1.Length + strPad.Length < strPart2.Length)
									strPad += " ";

								strPart1 = strPad + strPart1;
								}
							else
								{
								while (strPart2.Length + strPad.Length < strPart1.Length)
									strPad += " ";

								strPart2 = strPad + strPart2;
								}
							}

						rc = strPart1.CompareTo(strPart2);
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Case
				//----------------------------------------------------------------------------*

				case 6:
					rc = Item1.Load.Case.Manufacturer.CompareTo(Item2.Load.Case.Manufacturer);

					if (rc == 0)
						rc = Item1.Load.Case.Caliber.CompareTo(Item2.Load.Case.Caliber);

					if (rc == 0)
						{
						strPart1 = Item1.Load.Case.PartNumber;
						strPart2 = Item2.Load.Case.PartNumber;

						if (strPart1.Length != strPart2.Length)
							{
							string strPad = "";

							if (strPart1.Length < strPart2.Length)
								{
								while (strPart1.Length + strPad.Length < strPart2.Length)
									strPad += " ";

								strPart1 = strPad + strPart1;
								}
							else
								{
								while (strPart2.Length + strPad.Length < strPart1.Length)
									strPad += " ";

								strPart2 = strPad + strPart2;
								}
							}

						rc = strPart1.CompareTo(strPart2);
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Num Rounds
				//----------------------------------------------------------------------------*

				case 7:
					int nItem1 = 0;
					int nItem2 = 0;

					Int32.TryParse((Object1 as ListViewItem).Text, out nItem1);
					Int32.TryParse((Object2 as ListViewItem).Text, out nItem2);

					rc = nItem1.CompareTo(nItem2);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Best Group
				//----------------------------------------------------------------------------*

				case 8:
					rc = Item1.ChargeTest.BestGroup.CompareTo(Item2.ChargeTest.BestGroup);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// MOA
				//----------------------------------------------------------------------------*

				case 9:
					double Group1 = Item1.ChargeTest.BestGroup;
					double Group2 = Item2.ChargeTest.BestGroup;

					Group1 = cDataFiles.MetricToStandard(Group1, cDataFiles.eDataType.GroupSize);
					Group2 = cDataFiles.MetricToStandard(Group2, cDataFiles.eDataType.GroupSize);

					double dRange1 = Item1.ChargeTest.BestGroupRange;
					double dRange2 = Item2.ChargeTest.BestGroupRange;

					if (cPreferences.StaticPreferences.MetricRanges)
						{
						dRange1 = cConversions.MetersToYards(dRange1);
						dRange2 = cConversions.MetersToYards(dRange2);
						}

					double dMOA1 = (dRange1 > 0) ? Group1 / ((dRange1 / 100.0) * 1.047) : 0;
					double dMOA2 = (dRange2 > 0) ? Group2 / ((dRange2 / 100.0) * 1.047) : 0;

					rc = dMOA1.CompareTo(dMOA2);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Best Group Range
				//----------------------------------------------------------------------------*

				case 10:
					rc = Item1.ChargeTest.BestGroupRange.CompareTo(Item2.ChargeTest.BestGroupRange);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Muzzle Velocity
				//----------------------------------------------------------------------------*

				case 11:
					rc = Item1.ChargeTest.MuzzleVelocity.CompareTo(Item2.ChargeTest.MuzzleVelocity);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Max Deviation
				//----------------------------------------------------------------------------*

				case 12:
					Batch1 = m_DataFiles.GetBatchByID(Item1.ChargeTest.BatchID);
					Batch2 = m_DataFiles.GetBatchByID(Item2.ChargeTest.BatchID);

					if (Batch1 == null || Batch1.BatchTest == null || Batch1.BatchTest.TestShotList == null || Batch2 == null || Batch2.BatchTest == null || Batch2.BatchTest.TestShotList == null)
						rc = 0;
					else
						rc = Batch1.BatchTest.TestShotList.Statistics.MaxDeviation.CompareTo(Batch2.BatchTest.TestShotList.Statistics.MaxDeviation);

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Std Deviation
				//----------------------------------------------------------------------------*

				case 13:
					Batch1 = m_DataFiles.GetBatchByID(Item1.ChargeTest.BatchID);
					Batch2 = m_DataFiles.GetBatchByID(Item2.ChargeTest.BatchID);

					if (Batch1 == null || Batch1.BatchTest == null || Batch1.BatchTest.TestShotList == null || Batch2 == null || Batch2.BatchTest == null || Batch2.BatchTest.TestShotList == null)
						rc = 0;
					else
						rc = Batch1.BatchTest.TestShotList.Statistics.StdDev.CompareTo(Batch2.BatchTest.TestShotList.Statistics.StdDev);

					fSpecial = true;

					break;
				}

			if (fSpecial)
				{
				if (SortingOrder == SortOrder.Descending)
					return (0 - rc);

				return (rc);
				}

			return (base.Compare(Object1, Object2));
			}
		}
	}
