using Microsoft.Graphics.Canvas.UI.Xaml;
using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Perseverance_Calculator_1.Controller.Graphing.Graph2D
{
    internal class Graph2D
    {
        public void render2DDataX(CanvasControl sender, CanvasDrawEventArgs args, Formula formula, string data, double actualWidth, double actualHeight, double graphStepX, double graphStepY, double scale, double resolution, double x_location, double y_location, Color lineColor, (double[], double[]) drawRange, double axisSpacing, double circleRadius)
        {
            //double ellipseX = circleRadius;
            //double ellipseY = circleRadius;

            //if (graphStepX > graphStepY)
            //{
            //    ellipseX= ellipseX * graphStepY / graphStepX;
            //} 
            //else if (graphStepX < graphStepY)
            //{
            //    ellipseX = ellipseX * graphStepY / graphStepX;
            //    //ellipseY = ellipseY * graphStepX / graphStepY;
            //}



            float axisThickness = 2f;


            //string result = "";
            double LMin = drawRange.Item1[0];
            double LMax = drawRange.Item1[1];

            double RMin = drawRange.Item1[2];
            double RMax = drawRange.Item1[3];

            double BMin = drawRange.Item2[0];
            double BMax = drawRange.Item2[1];

            double TMin = drawRange.Item2[2];
            double TMax = drawRange.Item2[3];




            double x = actualWidth / 2 - x_location;//1000/2=500
            double y = actualHeight / 2 + y_location;//horline
            //double axisThickness = 2f;
            double useMinX = 0;
            double useMinY = 0;
            double minRangeX = 0;
            //double minRangeY = 0;

            double plotX = 0;
            if (RMin != -1 && RMax != -1)
            {
                useMinX = RMin;
                minRangeX = useMinX / graphStepX * axisSpacing * scale;
                plotX = 0 + x + minRangeX;
            }
            else if (LMin != -1 && LMax != -1)
            {
                useMinX = LMax;
                //minRange = -useMin * axisSpacing * scale;
                minRangeX = -useMinX / graphStepX * axisSpacing * scale;
                plotX = 0 + x - minRangeX;
            }
            else
            {
                minRangeX = 0;
                plotX = 0 + x + minRangeX;
            }

            //----
            double plotY = 0 + y;
            //double plotY = 0;
            //if (BMin != -1 && BMax != -1)
            //{
            //    useMinY = BMin;
            //    minRangeY = useMinY / graphStepX * axisSpacing * scale;
            //    plotY = 0 + y + minRangeY;
            //}
            //else if (TMin != -1 && TMax != -1)
            //{
            //    useMinY = TMax;
            //    //minRange = -useMin * axisSpacing * scale;
            //    minRangeY = -useMinY / graphStepX * axisSpacing * scale;
            //    plotY = 0 + y - minRangeY;
            //}
            //else
            //{
            //    minRangeY = 0;
            //    plotY = 0 + y + minRangeY;
            //}

            //float test = RMin;
            //if (RMin > 0)
            //{
            //    test -= xR;
            //}
            MathVue<double> mathV = new MathVue<double>();
            StringVue strV = new StringVue();

            //string dataSolution = mathV.solveFormula(formula, data, false);
            string dataSolution = data;

            //======================instance
            string instance_Substring = "";
            //string instance_splitDataY_Substring = "";
            string instance_splitDataX = "";
            string instance_splitDataY = "";
            bool containInstanceFunction = false;
            //bool containError = false;
            if (dataSolution.Contains("instance(", StringComparison.CurrentCulture))
            {
                int indexOfinstance = dataSolution.IndexOf("instance(", StringComparison.CurrentCulture);

                if (indexOfinstance - 1 == 0 ||
                    (indexOfinstance - 1 >= 0 &&
                    (new MathVue<double>().isOperator(dataSolution[indexOfinstance - 1]) ||
                    new MathVue<double>().isParenthesis(dataSolution[indexOfinstance - 1]) ||
                    new MathVue<double>().isComma(dataSolution[indexOfinstance - 1])

                    )))
                {
                    containInstanceFunction = true;
                    int indexOfinstance_Comma = strV.indexOf(dataSolution, ",", indexOfinstance + "instance(".Length, true);
                    int indexOfinstance_ClosePar = strV.indexOf(dataSolution, ")", indexOfinstance, true);

                    int endFunctionPar = strV.indexOf(dataSolution, ")", indexOfinstance, true);

                    instance_Substring = dataSolution.Substring(indexOfinstance, endFunctionPar - indexOfinstance + 1);

                    instance_splitDataX = dataSolution.Substring(indexOfinstance + "instance(".Length, (indexOfinstance_Comma - 1) - (indexOfinstance + ("instance(".Length - 1)));
                    instance_splitDataY = dataSolution.Substring(indexOfinstance_Comma + 1, (indexOfinstance_ClosePar) - (indexOfinstance_Comma + 1));

                    instance_splitDataX = mathV.solveFormula(formula, instance_splitDataX, false);
                    instance_splitDataY = mathV.solveFormula(formula, instance_splitDataY, false);
                }
                else return;/*containError = true;*/

            }

            //======================vec2

            int indexOfVec2 = dataSolution.IndexOf("vec2(", StringComparison.CurrentCulture);
            if(indexOfVec2==-1) return;
            int indexOfVec2_Comma = strV.indexOf(dataSolution, ",", indexOfVec2 + "vec2(".Length, true);
            int indexOf_ClosePar = strV.indexOf(dataSolution, ")", indexOfVec2, true);

            string splitDataX = dataSolution.Substring(indexOfVec2 + "vec2(".Length, (indexOfVec2_Comma - 1) - (indexOfVec2 + ("vec2(".Length - 1)));
            string splitDataY = dataSolution.Substring(indexOfVec2_Comma + 1, (indexOf_ClosePar) - (indexOfVec2_Comma + 1));

            //splitDataX = mathV.solveFormula(formula, splitDataX, false);
            //splitDataY = mathV.solveFormula(formula, splitDataY, false);

            //try
            //{
            if (containInstanceFunction)
            {
                for (int i = int.Parse(instance_splitDataX); i <= int.Parse(instance_splitDataY); i++)
                {

                    string splitDataX_Plot = mathV.solveFormula(formula, strV.replaceData(splitDataX, instance_Substring, i.ToString()).getNewData, false);
                    string splitDataY_Plot = mathV.solveFormula(formula, strV.replaceData(splitDataY, instance_Substring, i.ToString()).getNewData, false);

                    double plotX_Plot = plotX + (double.Parse(splitDataX_Plot, System.Globalization.NumberStyles.Any) - useMinX) * (axisSpacing * scale) / graphStepX;
                    double plotY_Plot = plotY - (double.Parse(splitDataY_Plot, System.Globalization.NumberStyles.Any) + useMinY) * (axisSpacing * scale) / graphStepY;
                    //plotY = y - axisSpacing * scale * double.Parse(new MathVue<double>().solveFormula(null, replaceData(data, "x", double.Parse(XYValue)), false), System.Globalization.NumberStyles.Any) / graphStepX;
                    if (plotX_Plot >= 0 - axisSpacing - (circleRadius * axisSpacing * scale / graphStepX) && plotX_Plot <= actualWidth + axisSpacing + (circleRadius * axisSpacing * scale / graphStepX) &&
                        plotY_Plot >= 0 - axisSpacing - (circleRadius * axisSpacing * scale / graphStepY) && plotY_Plot <= actualHeight + axisSpacing + (circleRadius * axisSpacing * scale / graphStepY))
                        //args.DrawingSession.DrawCircle(new Vector2((float)plotX_Plot, (float)plotY_Plot), (float)(circleRadius * axisSpacing * scale / graphStepX), lineColor,axisThickness);
                        args.DrawingSession.DrawEllipse(new Vector2((float)plotX_Plot, (float)plotY_Plot), (float)(circleRadius * axisSpacing * scale / graphStepX), (float)(circleRadius * axisSpacing * scale / graphStepY), lineColor, axisThickness);
                }
            }
            else
            {

                string splitDataX_Plot = mathV.solveFormula(formula, splitDataX, false);
                string splitDataY_Plot = mathV.solveFormula(formula, splitDataY, false);


                double parsedNumberX = 0;
                double parsedNumberY = 0;
                bool isParsedNumberX = double.TryParse(splitDataX_Plot, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumberX);
                bool isParsedNumberY = double.TryParse(splitDataY_Plot, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedNumberY);

                if (isParsedNumberX && isParsedNumberY)
                {
                    plotX = plotX + (parsedNumberX - useMinX) * (axisSpacing * scale) / graphStepX;
                    plotY = plotY - (parsedNumberY + useMinY) * (axisSpacing * scale) / graphStepY;
                    //plotY = y - axisSpacing * scale * double.Parse(new MathVue<double>().solveFormula(null, replaceData(data, "x", double.Parse(XYValue)), false), System.Globalization.NumberStyles.Any) / graphStepX;
                    if (plotX >= 0 - axisSpacing - (circleRadius * axisSpacing * scale / graphStepX) && plotX <= actualWidth + axisSpacing + (circleRadius * axisSpacing * scale / graphStepX) &&
                        plotY >= 0 - axisSpacing - (circleRadius * axisSpacing * scale / graphStepY) && plotY <= actualHeight + axisSpacing + (circleRadius * axisSpacing * scale / graphStepY))
                        //args.DrawingSession.DrawCircle(new Vector2((float)plotX, (float)plotY), (float)(circleRadius * axisSpacing * scale / graphStepX), lineColor, axisThickness);
                        args.DrawingSession.DrawEllipse(new Vector2((float)plotX, (float)plotY), (float)(circleRadius * axisSpacing * scale / graphStepX), (float)(circleRadius * axisSpacing * scale / graphStepY), lineColor, axisThickness);
                }
            }
            //args.DrawingSession.DrawRectangle(new Windows.Foundation.Rect(plotX, plotY, circleRadius * axisSpacing * scale / graphStepX, circleRadius * axisSpacing * scale / graphStepY), lineColor);

            //}
            //catch { }



        }
    }
}
