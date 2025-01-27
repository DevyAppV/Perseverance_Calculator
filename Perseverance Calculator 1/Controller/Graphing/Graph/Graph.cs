//using ColorCode.Compilation.Languages;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Perseverance_Calculator.View.Pages;
using Windows.UI.Core;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using System.Threading;
using Perseverance_Calculator_1.Model;
using System.Diagnostics;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using CommunityToolkit.Common.Parsers;

namespace Perseverance_Calculator_1.Controller.Graphing.Graph
{
    internal class Graph
    {
        //leftmin,Rightmax X, BottomMin,TopMaxY
        public (double[], double[]) renderAxis(CanvasControl sender, CanvasDrawEventArgs args, double actualWidth, double actualHeight, double graphStepX, double graphStepY, double scale, double x_location, double y_location, double axisSpacing)
        {
            (double[], double[]) result;
            CanvasTextFormat textFormat = new CanvasTextFormat();
            textFormat.FontSize = 25;
            //double axisSpacing = 100;

            //x_location = -100;
            ////y_location = 100;
            //scale = .5f;


            double axisThickness = 1f;
            double axisThickness_Supprt = 1f;

            double xL = /*Math.Clamp(*/actualWidth / 2 - x_location/*, 0, actualWidth)*/;//vertline
            double xR = /*Math.Clamp(*/actualWidth / 2 - x_location/*, 0, actualWidth)*/;
            double yT = /*Math.Clamp(*/actualHeight / 2 + y_location/*, 0, actualHeight)*/;//horline
            double yB = /*Math.Clamp(*/actualHeight / 2 + y_location/*, 0, actualHeight)*/;

            double countXL = -(double)graphStepX;
            double countXR = graphStepX;
            double countYB = -graphStepY;
            double countYT = graphStepY;

            string xL_AxisStringValue = countXL.ToString();
            string xR_AxisStringValue = countXR.ToString();
            string yB_AxisStringValue = countYB.ToString();
            string yT_AxisStringValue = countYT.ToString();

            double horizontalLineTextAlign_Pt = 30;
            double verticalLineTextAlign_Pt = 5;

            bool setOnce = false;



            double LMin = -1;
            double LMax = -1;

            double RMin = -1;
            double RMax = -1;

            double BMin = -1;
            double BMax = -1;

            double TMin = -1;
            double TMax = -1;

            bool setMinOnce = false;


            //horizontal
            if (actualHeight / 2 + y_location <= actualHeight && actualHeight / 2 + y_location >= 0)
            {
                args.DrawingSession.DrawLine(new System.Numerics.Vector2(0, (float)(actualHeight / 2 + y_location)), new System.Numerics.Vector2((float)actualWidth, (float)(actualHeight / 2 + y_location)), Colors.Black, (float)axisThickness);
                args.DrawingSession.DrawText("0", 0, (float)(actualHeight / 2 + y_location - horizontalLineTextAlign_Pt), Colors.Black, textFormat);
            }


            //vert - left line
            while (xL >= 0)
            {
                if (!setOnce && xL > actualWidth)
                {
                    setOnce = true;
                    double extendedWidth = actualWidth - xL;
                    if (extendedWidth < 0)
                    {
                        //extendedWidth = Math.Abs(actualWidth - xL);
                        //xL -= extendedWidth - (extendedWidth % (axisSpacing * scale));
                        //countXL = -(float)Math.Floor((extendedWidth - (extendedWidth % (axisSpacing * scale))) / axisSpacing) - graphStepX;

                        xL_AxisStringValue = (countXL = -(double)Math.Floor(Math.Abs(xL - actualWidth) / (axisSpacing * scale)) * graphStepX - graphStepX).ToString("E400", CultureInfo.InvariantCulture);
                        xL = (xL - actualWidth) % (axisSpacing * scale) + actualWidth;
                        LMax = countXL + graphStepX * 2;
                        LMax = (LMax > 0) ? 0 : LMax;

                        setMinOnce = true;
                    }
                }
                if (!setMinOnce)
                {
                    LMax = countXL + graphStepX;
                    setMinOnce = true;
                }

                //xL += actualWidth/2/5;
                xL -= axisSpacing * scale;
                if (xL <= actualWidth)
                {
                    //xL *= scale;
                    args.DrawingSession.DrawLine(new System.Numerics.Vector2((float)xL, 0), new System.Numerics.Vector2((float)xL, (float)actualHeight), new Color() { A = 100, B = 50, R = 50, G = 50 }, (float)axisThickness_Supprt);
                    args.DrawingSession.DrawText(new MathVue<double>().mathString_ToMathStringE(countXL.ToString()), (float)(xL + verticalLineTextAlign_Pt), (float)(actualHeight - horizontalLineTextAlign_Pt), Colors.Black, textFormat);
                }
                LMin = countXL;
                countXL -= graphStepX;
                //if((countXL - graphStepX).ToString().Contains("E"))
                //    xL_AxisStringValue = (countXL -= graphStepX).ToString("E400", CultureInfo.InvariantCulture);
                //else
                //    xL_AxisStringValue = (countXL -= graphStepX).ToString();

            }
            setMinOnce = false;
            setOnce = false;
            //vert - right line
            while (xR <= actualWidth)
            {
                if (!setOnce && xR < 0)
                {
                    setOnce = true;
                    double extendedWidth = actualWidth - xR;
                    if (extendedWidth > 0)
                    {
                        //xy_AxisStringValue = ((double)Math.Floor(Math.Abs((xR - 0) / (axisSpacing * scale))) * graphStepX + graphStepX).ToString("E400", CultureInfo.InvariantCulture);

                        xR_AxisStringValue = (countXR = (double)Math.Floor(Math.Abs((xR - 0) / (axisSpacing * scale))) * graphStepX + graphStepX).ToString("E400", CultureInfo.InvariantCulture);
                        //RMin = xR - ( actualWidth / 2 - x_location);
                        xR = (xR - 0) % (axisSpacing * scale) + 0;
                        RMin = countXR - graphStepX * 2;
                        RMin = (RMin < 0) ? 0 : RMin;

                        setMinOnce = true;
                    }
                }
                if (!setMinOnce)
                {
                    //RMin = xR - (actualWidth / 2 - x_location);
                    RMin = countXR - graphStepX;
                    setMinOnce = true;
                }
                //xR -= actualWidth/2/5;
                xR += axisSpacing * scale;
                //xR *= scale;
                args.DrawingSession.DrawLine(new System.Numerics.Vector2((float)xR, 0), new System.Numerics.Vector2((float)xR, (float)actualHeight), new Color() { A = 100, B = 50, R = 50, G = 50 }, (float)axisThickness_Supprt);
                args.DrawingSession.DrawText(new MathVue<double>().mathString_ToMathStringE(countXR.ToString()), (float)(xR + verticalLineTextAlign_Pt), (float)(actualHeight - horizontalLineTextAlign_Pt), Colors.Black, textFormat);

                RMax = countXR;
                countXR += graphStepX;
                //if ((countXR + graphStepX).ToString().Contains("E"))
                //    xR_AxisStringValue = (countXR += graphStepX).ToString("E400", CultureInfo.InvariantCulture);
                //else
                //    xR_AxisStringValue = (countXR += graphStepX).ToString();
            }



            //vertical
            if (actualWidth / 2 - x_location >= 0 && actualWidth / 2 - x_location <= actualWidth)
            {
                args.DrawingSession.DrawLine(new System.Numerics.Vector2((float)(actualWidth / 2 - x_location), 0), new System.Numerics.Vector2((float)(actualWidth / 2 - x_location), (float)actualHeight), Colors.Black, (float)axisThickness);
                args.DrawingSession.DrawText("0", (float)(actualWidth / 2 - x_location + verticalLineTextAlign_Pt), (float)(actualHeight - horizontalLineTextAlign_Pt), Colors.Black, textFormat);
            }
            setMinOnce = false;
            setOnce = false;
            //horizontal - bottom line
            while (yB <= actualHeight)
            {
                if (!setOnce && yB < 0)
                {
                    setOnce = true;
                    double extendedWidth = actualHeight - yB;
                    if (extendedWidth > 0)
                    {
                        //extendedWidth = Math.Abs(actualHeight + yB);
                        //yB += extendedWidth + (extendedWidth % (axisSpacing * scale));
                        //countYB = -(float)Math.Floor((extendedWidth + (extendedWidth % (axisSpacing * scale))) / axisSpacing) - graphStepX;

                        yB_AxisStringValue = (countYB = -(double)Math.Floor(Math.Abs(yB - 0) / (axisSpacing * scale)) * graphStepY - graphStepY).ToString("E400", CultureInfo.InvariantCulture); ;
                        yB = (yB - 0) % (axisSpacing * scale) + 0;
                        BMax = countYB + graphStepY * 2;
                        BMax = (BMax > 0) ? 0 : BMax;

                        setMinOnce = true;
                    }
                }
                if (!setMinOnce)
                {
                    BMax = countYB + graphStepY;
                    setMinOnce = true;
                }
                //yB += actualHeight / 2 / 5;
                yB += axisSpacing * scale;
                //yB *= scale;
                args.DrawingSession.DrawLine(new System.Numerics.Vector2(0, (float)yB), new System.Numerics.Vector2((float)actualWidth, (float)yB), new Color() { A = 100, B = 50, R = 50, G = 50 }, (float)axisThickness_Supprt);
                args.DrawingSession.DrawText(new MathVue<double>().mathString_ToMathStringE(countYB.ToString()), 0, (float)(yB - horizontalLineTextAlign_Pt), Colors.Black, textFormat);
                BMin = countYB;
                countYB -= graphStepY;

                //if ((countYB - graphStepX).ToString().Contains("E"))
                //    yB_AxisStringValue = (countYB -= graphStepX).ToString("E400", CultureInfo.InvariantCulture);
                //else
                //    yB_AxisStringValue = (countYB -= graphStepX).ToString();
            }
            setMinOnce = false;
            setOnce = false;
            //horizontal - top line
            while (yT >= 0)
            {
                if (!setOnce && yT > actualHeight)
                {
                    setOnce = true;
                    double extendedWidth = actualHeight - yT;
                    if (extendedWidth < 0)
                    {
                        //extendedWidth = Math.Abs(actualHeight - yT);
                        //yT -= extendedWidth - (extendedWidth % (axisSpacing * scale));
                        //countYT = (float)Math.Floor((extendedWidth - (extendedWidth % (axisSpacing * scale))) / axisSpacing) + graphStepX;

                        yT_AxisStringValue = (countYT = (double)Math.Floor(Math.Abs(yT - actualHeight) / (axisSpacing * scale)) * graphStepY + graphStepY).ToString("E400", CultureInfo.InvariantCulture); ;
                        yT = (yT - actualHeight) % (axisSpacing * scale) + actualHeight;
                        TMin = countYT - graphStepY * 2;
                        TMin = (TMin < 0) ? 0 : TMin;

                        setMinOnce = true;
                    }
                }
                if (!setMinOnce)
                {
                    TMin = countYT - graphStepY;
                    setMinOnce = true;
                }
                //yT -= actualHeight / 2 / 5;
                yT -= axisSpacing * scale;
                //yT *= scale;
                args.DrawingSession.DrawLine(new System.Numerics.Vector2(0, (float)yT), new System.Numerics.Vector2((float)actualWidth, (float)yT), new Color() { A = 100, B = 50, R = 50, G = 50 }, (float)axisThickness_Supprt);
                args.DrawingSession.DrawText(new MathVue<double>().mathString_ToMathStringE(countYT.ToString()), 0, (float)(yT - horizontalLineTextAlign_Pt), Colors.Black, textFormat);
                TMax = countYT;
                countYT += graphStepY;

                //if ((countYT + graphStepX).ToString().Contains("E"))
                //    yT_AxisStringValue = (countYT += graphStepX).ToString("E400", CultureInfo.InvariantCulture);
                //else
                //    yT_AxisStringValue = (countYT += graphStepX).ToString();
            }


            result = (new double[4] { LMin, LMax, RMin, RMax },
                 new double[4] { BMin, BMax, TMin, TMax });
            return result;
        }





        //public string replaceData(string data, string dataToReplace, string newData)
        //{


        //    int indexOfTime = 0;
        //    MathVue<double> mv = new MathVue<double>();
        //    try
        //    {
        //        if (data != null)
        //        {
        //            while (data.IndexOf(dataToReplace, indexOfTime, StringComparison.CurrentCulture) > -1)
        //            {
        //                indexOfTime = data.IndexOf(dataToReplace, StringComparison.CurrentCulture);
        //                if (indexOfTime - 1 >= 0 && indexOfTime + dataToReplace.Length <= data.Length - 1)
        //                {
        //                    if ((
        //                        mv.isOperator(data[indexOfTime - 1]) ||
        //                        mv.isComma(data[indexOfTime - 1]) ||
        //                        mv.isParenthesis(data[indexOfTime - 1])
        //                        ) &&
        //                        (
        //                        mv.isOperator(data[indexOfTime + dataToReplace.Length]) ||
        //                        mv.isComma(data[indexOfTime + dataToReplace.Length]) ||
        //                        mv.isParenthesis(data[indexOfTime + dataToReplace.Length])
        //                        ))
        //                    {
        //                        data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData); ;
        //                    }
        //                }
        //                else if (indexOfTime - 1 >= 0 && indexOfTime + dataToReplace.Length > data.Length - 1)
        //                {
        //                    if ((
        //                        mv.isOperator(data[indexOfTime - 1]) ||
        //                        mv.isComma(data[indexOfTime - 1]) ||
        //                        mv.isParenthesis(data[indexOfTime - 1])
        //                        ))
        //                    {
        //                        data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData); ;
        //                    }

        //                }
        //                else if (indexOfTime - 1 < 0 && indexOfTime + dataToReplace.Length <= data.Length - 1)
        //                {
        //                    if (
        //                        (
        //                        mv.isOperator(data[indexOfTime + dataToReplace.Length]) ||
        //                        mv.isComma(data[indexOfTime + dataToReplace.Length]) ||
        //                        mv.isParenthesis(data[indexOfTime + dataToReplace.Length])
        //                        ))
        //                    {
        //                        data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData);
        //                    }

        //                }
        //                else if (indexOfTime - 1 < 0 && indexOfTime + dataToReplace.Length > data.Length - 1)
        //                {
        //                    data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData);

        //                }
        //                indexOfTime++;
        //            }
        //            return data;
        //        }
        //    }
        //    catch { }

        //    return "";
        //}



        public string renderDataX_Point(Formula formula, CanvasControl sender, CanvasDrawEventArgs args, string data, double actualWidth, double actualHeight, double graphStepX, double graphStepY, double scale, double resolution, double x_location, double y_location, Color lineColor, (double[], double[]) drawRange, double axisSpacing, string XYValue)
        {

            string result = "";
            double LMin = drawRange.Item1[0];
            double LMax = drawRange.Item1[1];

            double RMin = drawRange.Item1[2];
            double RMax = drawRange.Item1[3];

            //double BMin = drawRange.Item2[0];
            //double BMax = drawRange.Item2[1];

            //double TMin = drawRange.Item2[2];
            //double TMax = drawRange.Item2[3];




            double x = actualWidth / 2 - x_location;//1000/2=500
            double y = actualHeight / 2 + y_location;//horline
            //double axisThickness = 2f;
            double useMin = 0;
            double minRange = 0;

            double plotX = 0;
            if (RMin != -1 && RMax != -1)
            {
                useMin = RMin;
                minRange = useMin / graphStepX * axisSpacing * scale;
                plotX = 0 + x + minRange;
            }
            else if (LMin != -1 && LMax != -1)
            {
                useMin = LMax;
                //minRange = -useMin * axisSpacing * scale;
                minRange = -useMin / graphStepX * axisSpacing * scale;
                plotX = 0 + x - minRange;
            }
            else
            {
                minRange = 0;
                plotX = 0 + x + minRange;
            }

            //float test = RMin;
            //if (RMin > 0)
            //{
            //    test -= xR;
            //}


            double plotY = 0 + y;
            //try
            //{

            StringVue strV = new StringVue();
            if (!string.IsNullOrWhiteSpace(XYValue))
            {

                //plotX = plotX + (double.Parse(XYValue, System.Globalization.NumberStyles.Any) - useMin) * (axisSpacing * scale) / graphStepX;

                double isNumber;
                if (double.TryParse(new MathVue<double>().solveFormula(formula, XYValue, false), out isNumber))
                {
                    double parsedNumber1 = 0;
                    bool isParsedNumber1 = double.TryParse(new MathVue<double>().solveFormula(formula, XYValue, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber1);

                    double parsedNumber2 = 0;
                    bool isParsedNumber2 = double.TryParse(new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", XYValue).getNewData, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber2);
                    if (isParsedNumber1 && isParsedNumber2)
                    {
                        plotX = plotX + (parsedNumber1 - useMin) * (axisSpacing * scale) / graphStepX;
                        //plotX = plotX + (double.Parse(new MathVue<double>().solveFormula(formula, XYValue, false), System.Globalization.NumberStyles.Any) - useMin) * (axisSpacing * scale) / graphStepX;
                        //plotY = y - axisSpacing * scale * double.Parse(new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", XYValue).getNewData, false), System.Globalization.NumberStyles.Any) / graphStepY;
                        plotY = y - axisSpacing * scale * parsedNumber2 / graphStepY;

                        args.DrawingSession.FillCircle(new Vector2((float)plotX, (float)plotY), 5f, lineColor);
                        //return result = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", XYValue).getNewData, false).ToString();
                        return result = parsedNumber2.ToString();
                    }
                }

            }
            //else
            //{
            //}
            //catch
            //{
            //try
            //{
            //StringVue strV = new StringVue();
            //return result = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", XYValue).getNewData, false).ToString();
            //}
            //catch { }
            //}
            //}

            return result;

        }

        //begindraw false = create new line
        public (double parsedNumber, bool drawNewLine, bool isParsed, bool isNotANumber) binarySearchFor_Asymptote(Formula formula, string data, double directionX_left, double directionX_right, bool isCalcFromLeftToRight = true)
        {

            //double xDeltaCheck_X = xDeltaCheck;
            //double betweenDelta_LineWidth_Radius = directionX_right - directionX_left;
            double leftPoint = directionX_left;
            double rightPoint = directionX_right;
            double rightPoint_Original = directionX_right;
            double leftPoint_Original = directionX_left;

            var asd = rightPoint - leftPoint;

            if (!isCalcFromLeftToRight)
            {

                leftPoint = directionX_right;
                rightPoint = directionX_left;
                rightPoint_Original = directionX_left;
                leftPoint_Original = directionX_right;
            }

            double mid = -1;
            string resultToParse;
            bool isParsed = true;
            double parsedNumber = 0;
            double parsedNumber_Result = 0;

            bool parsedOnce = false;

            StringVue strV = new StringVue();
            bool continueParsing = true;
            List<double> previousMid = new List<double>() { double.NaN, double.NaN };

            //double previousMid_Nested = 0;
            bool fin = false;
            //double setRightOnParsedFalse = -1;

            while (!fin && continueParsing && leftPoint <= rightPoint)
            {
                fin = false;
                continueParsing = false;
                mid = leftPoint + (rightPoint - leftPoint) / 2;

                if (previousMid[1] == mid && isCalcFromLeftToRight)
                {
                    mid += .000000000000001;
                    if (rightPoint == mid) fin = true;
                }
                else if (previousMid[1] == mid && !isCalcFromLeftToRight)
                {
                    mid -= .000000000000001;
                    if (rightPoint == mid) fin = true;
                }
                //var qwe = rightPoint - mid;
                //var zxc = leftPoint + betweenDelta_LineWidth_Radius;
                if (!fin && mid >= leftPoint_Original /*+ xDeltaCheck_X */&&
                    mid <= rightPoint_Original /*- xDeltaCheck_X*/)
                {
                    //bool setRight = false;
                    //bool doOnce =
                    do
                    {
                        //if (!isParsed)
                        //{
                        //    setRightOnParsedFalse = mid;
                        //}

                        //if (!isParsed && isCalcFromLeftToRight)
                        //{
                        //    setRightOnParsedFalse = mid;
                        //    rightPoint = setRightOnParsedFalse;
                        //}
                        //else if (!isParsed && !isCalcFromLeftToRight)
                        //{
                        //    setRightOnParsedFalse = mid;
                        //    leftPoint = setRightOnParsedFalse;
                        //}
                        resultToParse = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", mid.ToString()).getNewData, false);
                        isParsed = double.TryParse(resultToParse, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber);
                        if (!fin && !isParsed && isCalcFromLeftToRight)
                        {
                            //setRight = true;
                            if (rightPoint > mid)
                                rightPoint = mid;
                            else {
                                fin = true;
                                break; 
                            }
                            //previousMid_Nested = mid;
                            mid = leftPoint + (rightPoint - leftPoint) / 2;
                        }else if (!fin && !isParsed && !isCalcFromLeftToRight)
                        {
                            //setRight = true;
                            if (leftPoint < mid)
                                leftPoint = mid;
                            else
                            {
                                fin = true;
                                break;
                            }
                            leftPoint = mid;
                            mid = leftPoint + (rightPoint - leftPoint) / 2;
                        }
                        //else if (!parsedOnce && isParsed)
                        //{
                        //    parsedOnce = true;
                        //}
                    } while (!fin && !isParsed &&
                    (mid >= leftPoint_Original /*+ xDeltaCheck_X */&&
                    mid <= rightPoint_Original /*- xDeltaCheck_X*/));


                    if (isParsed && isCalcFromLeftToRight)
                    {
                        parsedOnce = true;
                        continueParsing = true;
                        parsedNumber_Result = parsedNumber;
                        leftPoint = mid;

                    }
                    else if (isParsed && !isCalcFromLeftToRight)
                    {
                        parsedOnce = true;
                        continueParsing = true;
                        parsedNumber_Result = parsedNumber;
                        rightPoint = mid;

                    }
                    else if (isCalcFromLeftToRight)
                    {
                        continueParsing = true;
                        //leftPoint = mid;
                        //leftPoint -= .000000000000001d;
                    }
                    else if (!isCalcFromLeftToRight)
                    {
                        continueParsing = true;
                        //rightPoint = mid;
                        //rightPoint += (rightPoint - leftPoint) / 10;

                    }
                    if (previousMid[0]!= double.NaN && previousMid[0] != previousMid[1])
                    {
                        previousMid.RemoveAt(0);
                        previousMid.Add(mid);
                    }
                    else fin = true;
                }
                else
                {
                    if (parsedOnce)
                        return (parsedNumber_Result, true, parsedOnce, false);
                    else
                        return (parsedNumber_Result, true, parsedOnce, true);
                }
            }
            //if (!isParsed)
            //{
            //    return (parsedNumber_Result, true, true, true);
            //}

            if (parsedOnce)
                return (parsedNumber_Result, true, parsedOnce, false);
            else
                return (parsedNumber_Result, true, parsedOnce, true);
            //return (parsedNumber_Result, true, parsedOnce, false);

        }

        public void renderDataX(Formula formula, CanvasControl sender, CanvasDrawEventArgs args, string data, double actualWidth, double actualHeight, double graphStepX, double graphStepY, double scale, double resolution, double x_location, double y_location, Color lineColor, (double[], double[]) drawRange, double axisSpacing)
        {
            //result = (new Vector2[2] { new Vector2(LMin, LMax), new Vector2(RMin, RMax) },
            //    new Vector2[2] { new Vector2(BMin, BMax), new Vector2(TMin, TMax) });

            //double asymptoteCheck_XDelta = .0000000000000001;


            double LMin = drawRange.Item1[0];
            double LMax = drawRange.Item1[1];

            double RMin = drawRange.Item1[2];
            double RMax = drawRange.Item1[3];

            //double BMin = drawRange.Item2[0];
            //double BMax = drawRange.Item2[1];

            //double TMin = drawRange.Item2[2];
            //double TMax = drawRange.Item2[3];




            double axisThickness = 2f;
            //float axisThickness_Supprt = 1f;


            //x_location = -100;
            ////y_location = 100;
            //scale = .5f;


            //float xL = Math.Clamp(actualWidth / 2 + x_location, 0, actualWidth);//vertline
            //float xR = Math.Clamp(actualWidth / 2 + x_location, 0, actualWidth);
            //float yT = Math.Clamp(actualHeight / 2 + y_location, 0, actualHeight);//horline
            //float yB = Math.Clamp(actualHeight / 2 + y_location, 0, actualHeight);

            //float xL = actualWidth / 2 - x_location;
            //float xR = actualWidth / 2 - x_location;
            //float yT = actualHeight / 2 + y_location;
            //float yB = actualHeight / 2 + y_location;



            //xR = (xR % (axisSpacing * scale)) - (actualWidth / 2) % (axisSpacing * scale) + 0;
            //xR = xR % (axisSpacing * scale) + 0;
            double useMin = 0;
            double minRange = 0;
            if (RMin != -1 && RMax != -1)
            {
                useMin = RMin;
                minRange = useMin / graphStepX * axisSpacing * scale;
            }
            else { minRange = 0; }
            //float test = RMin;
            //if (RMin > 0)
            //{
            //    test -= xR;
            //}

            double x = actualWidth / 2 - x_location;//1000/2=500
            double y = actualHeight / 2 + y_location;//horline

            double plotX = 0 + x + minRange;
            //float plotX = 0 + x;
            double plotY = 0 + y;
            double plotX2 = 0;
            double plotY2 = 0;

            //replace with x or y
            //float directionX = minRange / (axisSpacing*scale) * graphStepX;
            //float directionX = minRange / axisSpacing * resolution * scale * graphStepX;
            double directionX = minRange / (axisSpacing * scale) * graphStepX;
            //double xx = -1;
            //pos

            StringVue strV = new StringVue();
            //bool containsError = false;
            //MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary.Clear();

            var cpb = new CanvasPathBuilder(args.DrawingSession);


            //using (var cpb = new CanvasPathBuilder(args.DrawingSession))
            string resultToParse = "";
            double parsedNumber = 0;

            double previousDirectionX = directionX;
            //double previousParsedNumber = 0;

            bool isParsed = false;
            bool addedLine = false;

            (double parsedNumber, bool drawNewLine, bool isParsed, bool isNotANumber) asymptoteEndPoint = (0, false, false, false);
            bool isPointOnGraph = false;
            {

                //////////try
                //////////{
                resultToParse = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", directionX.ToString()).getNewData, false);
                isParsed = double.TryParse(resultToParse, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber);
                if (isParsed)
                {
                    //previousParsedNumber = parsedNumber;
                    plotY = y - axisSpacing * scale * parsedNumber / graphStepY;
                }
                //////////}
                //////////catch { /*containsError = true;*/ }
                //if (containsError)
                //    return;

                bool beginDraw = false;


                while (plotX <= actualWidth)
                {

                    plotX2 = plotX + (axisSpacing * resolution * scale) /** graphStepX*/;
                    //plotY2 = plotY - (axisSpacing * resolution * scale) * graphStepX;//linear correct: DONT USE
                    directionX += (plotX2 - plotX) / (axisSpacing * scale) * graphStepX;

                    //if (asymptoteEndPoint.drawNewLine)
                    //{
                    //    plotX = plotX2;
                    //    plotX2 = plotX + (axisSpacing * resolution * scale) /** graphStepX*/;
                    //    //plotY2 = plotY - (axisSpacing * resolution * scale) * graphStepX;//linear correct: DONT USE
                    //    directionX += (plotX2 - plotX) / (axisSpacing * scale) * graphStepX;

                    //    asymptoteEndPoint.drawNewLine = false;
                    //}

                    //plotY2 = y - axisSpacing * scale * double.Parse(new MathVue<double>().solveFormula(null, (Math.Tan(directionX) + directionX).ToString(), false)) / graphStepX;
                    //string s = "";
                    //string ss = "";
                    //////////try
                    //////////{
                    //s = strV.replaceData(data, "x", directionX.ToString());
                    //ss = new MathVue<double>().solveFormula(null, strV.replaceData(data, "x", directionX.ToString()), false);
                    resultToParse = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", directionX.ToString()).getNewData, false);
                    isParsed = double.TryParse(resultToParse, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber);


                    if (isParsed)
                    {
                        if (!addedLine)
                            addedLine = true;
                        plotY2 = y - axisSpacing * scale * parsedNumber / graphStepY;


                        if ((plotY2 >= 0 && plotY <= 0/* && plotY2 - plotY > actualHeight*/) ||
                            (plotY2 <= 0 && plotY >= 0/* && plotY2 - plotY < -actualHeight*/))
                        {

                            asymptoteEndPoint = binarySearchFor_Asymptote(formula, data, previousDirectionX, directionX, true);

                            if (asymptoteEndPoint.isParsed && !asymptoteEndPoint.isNotANumber)
                            {
                                if (!addedLine)
                                    addedLine = true;
                                plotY2 = y - axisSpacing * scale * asymptoteEndPoint.parsedNumber / graphStepY;


                                if (asymptoteEndPoint.drawNewLine)
                                {
                                    if (beginDraw)
                                    {
                                        cpb.EndFigure(CanvasFigureLoop.Open);
                                        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(cpb), lineColor, (float)axisThickness);

                                        cpb = new CanvasPathBuilder(args.DrawingSession);
                                        beginDraw = false;
                                    }
                                }
                                else
                                {
                                    isPointOnGraph = true;
                                    if (!beginDraw)
                                    {
                                        //if (isTanAsymptote) continue;
                                        cpb.BeginFigure(new Vector2((float)plotX, (float)plotY));
                                        beginDraw = true;
                                        //continue;
                                    }
                                    else
                                        cpb.AddLine(new Vector2((float)plotX, (float)plotY));
                                    cpb.AddLine(new Vector2((float)plotX2, (float)plotY2));
                                }

                            }
                        }
                        else
                        {
                            isPointOnGraph = true;
                            if (!beginDraw)
                            {
                                //if (isTanAsymptote) continue;
                                cpb.BeginFigure(new Vector2((float)plotX, (float)plotY));
                                beginDraw = true;
                                //continue;
                            }
                            else
                                cpb.AddLine(new Vector2((float)plotX, (float)plotY));
                            cpb.AddLine(new Vector2((float)plotX2, (float)plotY2));
                        }
                    }
                    else
                    {
                        if (isPointOnGraph)
                        {

                            asymptoteEndPoint = binarySearchFor_Asymptote(formula, data, previousDirectionX, directionX, true);

                            if (asymptoteEndPoint.isParsed && !asymptoteEndPoint.isNotANumber)
                            {
                                if (!addedLine)
                                    addedLine = true;
                                plotY2 = y - axisSpacing * scale * asymptoteEndPoint.parsedNumber / graphStepY;

                                if (!beginDraw)
                                {
                                    //if (isTanAsymptote) continue;
                                    cpb.BeginFigure(new Vector2((float)plotX, (float)plotY));
                                    beginDraw = true;
                                    //continue;
                                }
                                else
                                    cpb.AddLine(new Vector2((float)plotX, (float)plotY));
                                cpb.AddLine(new Vector2((float)plotX2, (float)plotY2));
                                //}

                                if (asymptoteEndPoint.drawNewLine)
                                {
                                    if (beginDraw)
                                    {
                                        cpb.EndFigure(CanvasFigureLoop.Open);
                                        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(cpb), lineColor, (float)axisThickness);

                                        cpb = new CanvasPathBuilder(args.DrawingSession);
                                        beginDraw = false;
                                    }
                                }

                            }
                            isPointOnGraph = false;
                        }
                    }

                    plotX = plotX2;
                    plotY = plotY2;

                    previousDirectionX = directionX;
                    //previousParsedNumber = parsedNumber;
                }
                //try
                //{

                if (addedLine)
                {
                    if (beginDraw)
                    {
                        cpb.EndFigure(CanvasFigureLoop.Open);
                        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(cpb), lineColor, (float)axisThickness);
                    }
                }
                //}
                //catch { }
            }

            if (LMin != -1 && LMax != -1)
            {
                useMin = LMax;
                //minRange = -useMin * axisSpacing * scale;
                minRange = -useMin / graphStepX * axisSpacing * scale;
            }
            else { minRange = 0; }

            plotX = x - minRange;
            plotY = 0 + y;
            plotX2 = 0;
            plotY2 = 0;

            //directionX = 0;
            //directionX = minRange / (axisSpacing * scale) * graphStepX;
            //directionX = minRange / -axisSpacing;
            directionX = minRange / -(axisSpacing * scale) * graphStepX;
            previousDirectionX = directionX;
            isPointOnGraph = false;

            asymptoteEndPoint = (0, false, false, false);
            cpb = new CanvasPathBuilder(args.DrawingSession);
            //using (var cpb = new CanvasPathBuilder(args.DrawingSession))
            {
                resultToParse = "";
                parsedNumber = 0;
                isParsed = false;
                addedLine = false;
                //////////try
                //////////{
                resultToParse = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", directionX.ToString()).getNewData, false);
                isParsed = double.TryParse(resultToParse, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber);
                if (isParsed)
                    plotY = y - axisSpacing * scale * parsedNumber / graphStepY;
                //////////}
                //////////catch { /*containsError = true;*/ }
                //if (containsError)
                //    return;

                bool beginDraw = false;
                //neg
                while (plotX >= 0)
                {
                    plotX2 = plotX - (axisSpacing * resolution * scale) /** graphStepX*/;
                    //plotY2 = plotY + (axisSpacing * resolution * scale) * graphStepX;  //linear correct: DONT USE
                    directionX -= (plotX2 - plotX) / -(axisSpacing * scale) * graphStepX;
                    //plotY2 = y - axisSpacing * scale * double.Parse(new MathVue<double>().solveFormula(null, (Math.Tan(directionX)).ToString(), false)) / graphStepX;
                    //////////try
                    //////////{

                    //parsedNumber = 0;
                    resultToParse = new MathVue<double>().solveFormula(formula, strV.replaceData(data, "x", directionX.ToString()).getNewData, false);
                    isParsed = double.TryParse(resultToParse, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumber);
                    if (isParsed)
                    {
                        if (!addedLine)
                            addedLine = true;
                        plotY2 = y - axisSpacing * scale * parsedNumber / graphStepY;
                        if ((plotY2 >= 0 && plotY <= 0 && plotY2 - plotY > actualHeight) ||
                            (plotY2 <= 0 && plotY >= 0 && plotY2 - plotY < -actualHeight))
                        {

                            asymptoteEndPoint = binarySearchFor_Asymptote(formula, data, previousDirectionX, directionX, false);

                            if (asymptoteEndPoint.isParsed && !asymptoteEndPoint.isNotANumber)
                            {
                                if (!addedLine)
                                    addedLine = true;
                                plotY2 = y - axisSpacing * scale * asymptoteEndPoint.parsedNumber / graphStepY;


                                if (asymptoteEndPoint.drawNewLine)
                                {
                                    if (beginDraw)
                                    {
                                        cpb.EndFigure(CanvasFigureLoop.Open);
                                        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(cpb), lineColor, (float)axisThickness);

                                        cpb = new CanvasPathBuilder(args.DrawingSession);
                                        beginDraw = false;
                                    }
                                }
                                else
                                {
                                    isPointOnGraph = true;
                                    if (!beginDraw)
                                    {
                                        //if (isTanAsymptote) continue;
                                        cpb.BeginFigure(new Vector2((float)plotX, (float)plotY));
                                        beginDraw = true;
                                        //continue;
                                    }
                                    else
                                        cpb.AddLine(new Vector2((float)plotX, (float)plotY));
                                    cpb.AddLine(new Vector2((float)plotX2, (float)plotY2));
                                }

                            }
                        }
                        else
                        {
                            isPointOnGraph = true;
                            if (!beginDraw)
                            {
                                cpb.BeginFigure(new Vector2((float)plotX, (float)plotY));
                                beginDraw = true;
                            }
                            else
                                cpb.AddLine(new Vector2((float)plotX, (float)plotY));
                            cpb.AddLine(new Vector2((float)plotX2, (float)plotY2));
                        }

                    }
                    else
                    {
                        if (isPointOnGraph)
                        {

                            asymptoteEndPoint = binarySearchFor_Asymptote(formula, data, previousDirectionX, directionX, false);

                            if (asymptoteEndPoint.isParsed && !asymptoteEndPoint.isNotANumber)
                            {
                                if (!addedLine)
                                    addedLine = true;
                                plotY2 = y - axisSpacing * scale * asymptoteEndPoint.parsedNumber / graphStepY;


                                if (!beginDraw)
                                {
                                    //if (isTanAsymptote) continue;
                                    cpb.BeginFigure(new Vector2((float)plotX, (float)plotY));
                                    beginDraw = true;
                                    //continue;
                                }
                                else
                                    cpb.AddLine(new Vector2((float)plotX, (float)plotY));
                                cpb.AddLine(new Vector2((float)plotX2, (float)plotY2));
                                //}

                                if (asymptoteEndPoint.drawNewLine)
                                {
                                    if (beginDraw)
                                    {
                                        cpb.EndFigure(CanvasFigureLoop.Open);
                                        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(cpb), lineColor, (float)axisThickness);

                                        cpb = new CanvasPathBuilder(args.DrawingSession);
                                        beginDraw = false;
                                    }
                                }

                            }
                            isPointOnGraph = false;
                        }
                    }

                    plotX = plotX2;
                    plotY = plotY2;

                    previousDirectionX = directionX;
                }
                //try
                //{
                if (addedLine)
                {
                    if (beginDraw)
                    {
                        cpb.EndFigure(CanvasFigureLoop.Open);
                        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(cpb), lineColor, (float)axisThickness);
                    }
                }
                //}
                //catch { }
            }




        }

    }
}
