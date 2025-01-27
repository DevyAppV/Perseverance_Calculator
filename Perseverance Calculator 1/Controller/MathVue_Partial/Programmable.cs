using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perseverance_Calculator_1.Controller
{
    internal partial class MathVue<T>
    {

        private string orThen(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread)
        {
            bool boolResult = false;
            //try
            //{
            if (splitStr["x"].Contains("true") || splitStr["y"].Contains("true"))
            {
                boolResult = true;
            }

            if (boolResult)
            {
                //if (splitStr["trueReturn"].Contains("throw"))
                //{
                //    //throw new Exception(splitStr["trueReturn"]);
                //    return splitStr["trueReturn"];
                //}
                return splitStr["trueReturn"];
            }
            else
            {
                //if (splitStr["falseReturn"].Contains("throw"))
                //{
                //    //throw new Exception(splitStr["falseReturn"]);
                //    return splitStr["falseReturn"];
                //}
                return splitStr["falseReturn"];
            }
            //}
            //catch
            //{
            //    throw new Exception("Program Error");
            //}
        }
        private string ifThen(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread)
        {
            bool boolResult = false;

            //try
            //{

            
            if (typeof(T).Equals(typeof(double)))
            {
                double x = 0;
                double y = 0;
                bool xParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x);


                bool yParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["y"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out y);


                //double x = double.Parse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any);


                //double y = double.Parse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["y"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any);
                if (xParsed && yParsed)
                {
                    switch (splitStr["rOperator"])
                    {
                        case ">":
                            if (x > y)
                                boolResult = true;
                            break;
                        case "<":
                            if (x < y)
                                boolResult = true;
                            break;
                        case ">=":
                            if (x >= y)
                                boolResult = true;
                            break;
                        case "<=":
                            if (x <= y)
                                boolResult = true;
                            break;
                        case "==":
                            if (x == y)
                                boolResult = true;
                            break;
                        case "!=":
                            if (x != y)
                                boolResult = true;
                            break;
                        default:
                            break;

                    }
                }
                else if ((splitStr["x"].Contains("true") || splitStr["x"].Contains("false")) && (splitStr["y"].Contains("true") || splitStr["y"].Contains("false")))
                {
                    bool xx = false;
                    bool yy = false;
                    if (splitStr["x"].Contains("true"))
                        xx = true;
                    if (splitStr["y"].Contains("true"))
                        yy = true;

                    string s = splitStr["rOperator"];
                    switch (splitStr["rOperator"])
                    {
                        case "==":
                            if (xx == yy)
                                boolResult = true;
                            break;
                        case "!=":
                            if (xx != yy)
                                boolResult = true;
                            break;
                        default:
                            boolResult = false;
                            break;

                    }
                }
                else return "Program Error";
            }
            else
            {

                decimal x = 0;
                decimal y = 0;
                bool xParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x);


                bool yParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["y"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out y);
                if (xParsed && yParsed)
                {
                    switch (splitStr["rOperator"])
                    {
                        case ">":
                            if (x > y)
                                boolResult = true;
                            break;
                        case "<":
                            if (x < y)
                                boolResult = true;
                            break;
                        case ">=":
                            if (x >= y)
                                boolResult = true;
                            break;
                        case "<=":
                            if (x <= y)
                                boolResult = true;
                            break;
                        case "==":
                            if (x == y)
                                boolResult = true;
                            break;
                        case "!=":
                            if (x != y)
                                boolResult = true;
                            break;
                        default:
                            break;

                    }

                }
                else if ((splitStr["x"].Contains("true") || splitStr["x"].Contains("false")) && (splitStr["y"].Contains("true") || splitStr["y"].Contains("false")))
                {
                    bool xx = false;
                    bool yy = false;
                    if (splitStr["x"].Contains("true"))
                        xx = true;
                    if (splitStr["y"].Contains("true"))
                        yy = true;

                    string s = splitStr["rOperator"];
                    switch (splitStr["rOperator"])
                    {
                        case "==":
                            if (xx == yy)
                                boolResult = true;
                            break;
                        case "!=":
                            if (xx != yy)
                                boolResult = true;
                            break;
                        default:
                            boolResult = false;
                            break;

                    }
                }
                else return "Program Error";
            }
            //}
            //catch
            //{
            //    throw new Exception("Program Error");
            //}

            if (boolResult)
            {
                //if (splitStr["trueReturn"].Contains("throw"))
                //{
                //    //throw new Exception(splitStr["trueReturn"]);
                //    return splitStr["trueReturn"];
                //}
                return splitStr["trueReturn"];
            }
            else
            {
                //if (splitStr["falseReturn"].Contains("throw"))
                //{
                //    //throw new Exception(splitStr["falseReturn"]);
                //    return splitStr["falseReturn"];
                //}
                return splitStr["falseReturn"];
            }
        }
    }
}
