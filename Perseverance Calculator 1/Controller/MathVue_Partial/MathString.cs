using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//************************************************************************** REMOVE MathVue_Partial from namespace ****************************************************
//************************************************************************** REMOVE MathVue_Partial from namespace ****************************************************
//************************************************************************** REMOVE MathVue_Partial from namespace ****************************************************

//namespace Perseverance_Calculator_1.Controller.MathVue_Partial
namespace Perseverance_Calculator_1.Controller
{
    internal partial class MathVue<T>
    {//==================================================================Simple String Math
        #region ERROR
        public enum STRING_MATH_OPTION
        {
            add,
            subtract,
            divide,
            multiply
        }


        public string stringMath(string refString1, string refString2, STRING_MATH_OPTION mathOperationType)
        {
            string result = "";

            refString1 = mathE_ToString(refString1);
            refString2 = mathE_ToString(refString2);

            double ref1 = double.Parse(refString1);
            double ref2 = double.Parse(refString2);

            if (mathOperationType == STRING_MATH_OPTION.add)
            {
                //if (ref1 < 0 && ref2 < 0)
                //{
                //    result = "-" + stringAdd(refString1.Replace("-", "").TrimStart('0'), refString2.Replace("-", "").TrimStart('0'));
                //}
                //else if (ref1 >= 0 && ref2 >= 0)
                //{
                //    result = stringAdd(refString1.TrimStart('0'), refString2.TrimStart('0'));
                //}
                //else if (ref1 >= 0 && ref2 <= 0 || ref1 <= 0 && ref2 >= 0)
                //{
                //    result = stringSubtract(refString1.TrimStart('0'), refString2.TrimStart('0'));
                //}
                result = stringAdd(refString1, refString2);
            }
            else if (mathOperationType == STRING_MATH_OPTION.subtract)
            {
                //if (ref1 <= 0 && ref2 <= 0)
                //{
                //    result = stringSubtract(refString1.TrimStart('0'), refString2.Replace("-", "").TrimStart('0'));

                //}
                //else if (ref1 >= 0 && ref2 <= 0)
                //{
                //    result = stringAdd(refString1.TrimStart('0'), refString2.Replace("-", "").TrimStart('0'));
                //}
                //else if (ref1 <= 0 && ref2 >= 0)
                //{
                //    result = "-" + stringAdd(refString1.Replace("-", "").TrimStart('0'), refString2.TrimStart('0'));
                //}
                //else if (ref1 >= 0 && ref2 >= 0)
                //{
                //    result = stringSubtract(refString1.TrimStart('0'), "-"+refString2.TrimStart('0'));
                //}
                result = stringSubtract(refString1, refString2);
            }


            return result;
        }


        private string mathE_ToString(string refString)
        {
            string result = refString;
            int indexOfEP = refString.IndexOf("E+");
            int indexOfEN = refString.IndexOf("E-");

            int indexOfE = (indexOfEP > indexOfEN) ? indexOfEP : -1;

            if (indexOfE == -1)
            {
                indexOfE = (indexOfEP < indexOfEN) ? indexOfEN : -1;
            }
            if (indexOfE > -1)
            {
                result = refString.Remove(indexOfE);

                int numberOfE = int.Parse(refString.Remove(0, indexOfE + 2));
                int indexOfDecimal = result.IndexOf(".");

                int lengthBeforeDecimal = result.Remove(indexOfDecimal).Length;
                int lengthAfterDecimal = result.Remove(0, indexOfDecimal + 1).Length;
                int numOf0_ToAdd = numberOfE - lengthAfterDecimal;
                int decimalPosition = lengthBeforeDecimal + numberOfE;

                while (numOf0_ToAdd > 0)
                {
                    result += "0";
                    numOf0_ToAdd--;
                }
                result = result.Replace(".", "").Insert(decimalPosition, ".");

            }

            return result;

        }


        private string stringAdd(string refString1, string refString2)
        {
            string new_RefString1 = (refString1.Equals("") || refString1.Equals(".")) ? "0" : refString1
                .Replace("-", "").TrimStart('0');
            if (new_RefString1.StartsWith(".") || string.IsNullOrEmpty(new_RefString1))
                new_RefString1 = new_RefString1.Insert(0, "0");

            string new_RefString2 = (refString2.Equals("") || refString2.Equals(".")) ? "0" : refString2
                .Replace("-", "").TrimStart('0');

            if (new_RefString2.StartsWith(".") || string.IsNullOrEmpty(new_RefString2))
                new_RefString2 = new_RefString2.Insert(0, "0");

            string result = "";
            string ref1 = new_RefString1;
            string ref2 = new_RefString2;
            int indexOfDecimal_Str1 = ref1.IndexOf(".");
            int indexOfDecimal_Str2 = ref2.IndexOf(".");



            if (indexOfDecimal_Str1 == -1)
            {
                ref1 = ref1.Insert(ref1.Length, ".");
            }
            if (indexOfDecimal_Str2 == -1)
            {
                ref2 = ref2.Insert(ref2.Length, ".");
            }

            int ref1_LengthAfterDecimal = ref1.Remove(0, ref1.IndexOf(".") + 1).Length;
            int ref2_LengthAfterDecimal = ref2.Remove(0, ref2.IndexOf(".") + 1).Length;

            int numOf0_ToAdd = ref1_LengthAfterDecimal - ref2_LengthAfterDecimal;

            if (numOf0_ToAdd == 0) { }
            else if (numOf0_ToAdd < 0)
            {
                while (numOf0_ToAdd < 0)
                {
                    ref1 += "0";
                    numOf0_ToAdd++;
                }
            }
            else if (numOf0_ToAdd > 0)
            {
                while (numOf0_ToAdd > 0)
                {
                    ref2 += "0";
                    numOf0_ToAdd--;
                }
            }
            int str1Len = ref1.Length;
            int str2Len = ref2.Length;

            int carryOver = 0;
            string char1 = "";
            string char2 = "";

            if (refString1.StartsWith('-') && !refString2.StartsWith('-'))
            {
                result = stringSubtract(refString1, "-" + refString2);
            }
            else if (!refString1.StartsWith('-') && refString2.StartsWith('-'))
            {
                result = stringSubtract(refString1, refString2.Replace("-", ""));
            }
            else
            {
                while (str1Len > 0 || str2Len > 0)
                {
                    char1 = (str1Len <= 0) ? 0.ToString() : ref1[str1Len - 1].ToString();
                    char2 = (str2Len <= 0) ? 0.ToString() : ref2[str2Len - 1].ToString();


                    if (!char1.Equals(".") && !char2.Equals("."))
                    {
                        int valResult = 0;
                        int val1 = int.Parse(char1);
                        int val2 = int.Parse(char2);
                        valResult = val1 + val2 + carryOver;

                        result = result.Insert(0, (valResult % 10).ToString());
                        if (valResult >= 10)
                        {
                            carryOver = (valResult - (valResult % 10)) / 10;
                        }
                        else
                        {
                            carryOver = 0;
                        }
                    }
                    else
                    {
                        result = result.Insert(0, ".");
                    }

                    str1Len--;
                    str2Len--;
                }
            }
            if (carryOver > 0)
            {
                result = result.Insert(0, carryOver.ToString());
            }

            result = result.TrimStart('0');
            if (refString1.StartsWith('-') && refString2.StartsWith('-'))
            {
                result = result.Insert(0, "-");
            }
            if (result.Equals("-."))
                result = "0";
            return result;
        }






        private string stringSubtract(string refString1, string refString2)
        {
            string new_RefString1 = (refString1.Equals("") || refString1.Equals(".")) ? "0" : refString1
                .Replace("-", "").TrimStart('0');
            if (new_RefString1.StartsWith(".") || string.IsNullOrEmpty(new_RefString1))
                new_RefString1 = new_RefString1.Insert(0, "0");

            string new_RefString2 = (refString2.Equals("") || refString2.Equals(".")) ? "0" : refString2
                .Replace("-", "").TrimStart('0');

            if (new_RefString2.StartsWith(".") || string.IsNullOrEmpty(new_RefString2))
                new_RefString2 = new_RefString2.Insert(0, "0");


            string result = "";
            string ref1 = new_RefString1;
            string ref2 = new_RefString2;
            int indexOfDecimal_Str1 = ref1.IndexOf(".");
            int indexOfDecimal_Str2 = ref2.IndexOf(".");



            if (indexOfDecimal_Str1 == -1)
            {
                ref1 = ref1.Insert(ref1.Length, ".");
            }
            if (indexOfDecimal_Str2 == -1)
            {
                ref2 = ref2.Insert(ref2.Length, ".");
            }

            int ref1_LengthAfterDecimal = ref1.Remove(0, ref1.IndexOf(".") + 1).Length;
            int ref2_LengthAfterDecimal = ref2.Remove(0, ref2.IndexOf(".") + 1).Length;

            int numOf0_ToAdd = ref1_LengthAfterDecimal - ref2_LengthAfterDecimal;

            if (numOf0_ToAdd == 0) { }
            else if (numOf0_ToAdd < 0)
            {
                while (numOf0_ToAdd < 0)
                {
                    ref1 += "0";
                    numOf0_ToAdd++;
                }
            }
            else if (numOf0_ToAdd > 0)
            {
                while (numOf0_ToAdd > 0)
                {
                    ref2 += "0";
                    numOf0_ToAdd--;
                }
            }


            int str1Len = ref1.Length;
            int str2Len = ref2.Length;

            int takeAway = 0;
            string char1 = "";
            string char2 = "";

            //bool isRef1GreaterEqualToRef2 = (str1Len >= str2Len) ? true : false;

            bool isNegativeReturn = false;

            if (!refString1.StartsWith('-') && refString2.StartsWith('-'))
            {
                result = stringAdd(refString1, refString2.Replace("-", ""));
            }
            else
            {
                while (str1Len > 0 || str2Len > 0)
                {
                    char1 = (str1Len <= 0) ? 0.ToString() : ref1[str1Len - 1].ToString();
                    char2 = (str2Len <= 0) ? 0.ToString() : ref2[str2Len - 1].ToString();


                    if (!char1.Equals(".") && !char2.Equals("."))
                    {
                        int valResult = 0;
                        int val1 = int.Parse(char1);
                        int val2 = int.Parse(char2);


                        if (str1Len == str2Len)
                        {
                            if (double.Parse(ref1) >= double.Parse(ref2))
                            {
                                int Val1_Borrow = (val1 - takeAway < val2) ? val1 + 10 : val1;
                                valResult = Val1_Borrow - val2 - takeAway;
                                if (Val1_Borrow >= 10)
                                {
                                    takeAway = 1;
                                }
                                else takeAway = 0;

                                if (!isNegativeReturn && refString1.StartsWith('-'))
                                    isNegativeReturn = true;
                            }
                            else
                            {
                                int Val2_Borrow = (val2 - takeAway < val1) ? val2 + 10 : val2;
                                valResult = Val2_Borrow - val1 - takeAway;
                                if (Val2_Borrow >= 10)
                                {
                                    takeAway = 1;
                                }
                                else takeAway = 0;

                                if (!isNegativeReturn && !refString2.StartsWith('-'))
                                    isNegativeReturn = true;
                            }
                        }

                        else if (str1Len > str2Len)
                        {
                            int Val1_Borrow = (val1 - takeAway < val2) ? val1 + 10 : val1;
                            valResult = Val1_Borrow - val2 - takeAway;
                            if (Val1_Borrow >= 10)
                            {
                                takeAway = 1;
                            }
                            else takeAway = 0;

                            if (!isNegativeReturn && refString1.StartsWith('-'))
                                isNegativeReturn = true;
                        }
                        else if (str2Len > str1Len)
                        {
                            int Val2_Borrow = (val2 - takeAway < val1) ? val2 + 10 : val2;
                            valResult = Val2_Borrow - val1 - takeAway;
                            if (Val2_Borrow >= 10)
                            {
                                takeAway = 1;
                            }
                            else takeAway = 0;


                            if (!isNegativeReturn && !refString2.StartsWith('-'))
                                isNegativeReturn = true;
                        }

                        result = result.Insert(0, (Math.Abs(valResult)).ToString());

                    }
                    else
                    {
                        result = result.Insert(0, ".");
                    }

                    str1Len--;
                    str2Len--;
                }
            }


            result = result.TrimStart('0');
            //if (double.Parse(ref1) > double.Parse(ref2) && double.Parse(refString1) < 0)
            //{
            //    result = result.Insert(0, "-");
            //}
            //else if (double.Parse(ref2) > double.Parse(ref1) && double.Parse(refString2) < 0)
            //{
            //    result = result.Insert(0, "-");
            //}
            if (isNegativeReturn)
                result = result.Insert(0, "-");
            return result;
        }



        #endregion ERROR











        //==================================================================MathString To MathString E

        public string mathString_ToMathStringE(string mathString, int acceptableNumberLength = 5)
        {
            if (!string.IsNullOrWhiteSpace(mathString))
            {
                //int acceptableNumberLength = 5;
                string result = mathString;
                int indexOfDecimal = mathString.IndexOf('.');
                int lengthOf_MathString = result.Length;
                bool startsWithNeg = (mathString[0].Equals('-')) ? true : false;

                int indexOf_EMinus = result.IndexOf("E-");
                int indexOf_EPlus = result.IndexOf("E");
                int numberOfE = 0;
                if (indexOf_EMinus >= 0)
                {
                    numberOfE = int.Parse(result.Remove(0, indexOf_EMinus + 1));
                    result = result.Remove(indexOf_EMinus);
                }
                else if (indexOf_EPlus >= 0)
                {
                    numberOfE = int.Parse(result.Remove(0, indexOf_EPlus + 1));
                    result = result.Remove(indexOf_EPlus);
                }

                if (lengthOf_MathString > acceptableNumberLength || indexOf_EMinus >= 0 || indexOf_EPlus >= 0)
                {

                    if (indexOfDecimal == -1)
                    {
                        result += ".";
                        indexOfDecimal = result.IndexOf('.');
                    }
                    if (startsWithNeg)
                    {
                        indexOfDecimal--;
                        result = result.Remove(0, 1).Remove(indexOfDecimal, 1);
                    }
                    else
                    {
                        result = result.Remove(indexOfDecimal, 1);
                    }

                    int i = 0;
                    while (result[i].Equals('0'))
                    {
                        i++;
                        if (i >= result.Length) break;
                    }

                    i++;
                    int eToPowerOf = i - indexOfDecimal + numberOfE;
                    if (eToPowerOf == 0)
                    {
                        //int length = lengthOf_MathString - acceptableNumberLength;
                        if (startsWithNeg)
                        {
                            if (result.Length > acceptableNumberLength)
                            {
                                result = "-" + result.Remove(acceptableNumberLength).Insert(indexOfDecimal, ".") /*+ length*/; ;
                            }
                            else
                            {
                                result = "-" + result.Insert(indexOfDecimal, ".") /*+ length*/;
                            }
                        }
                        else
                        {
                            if (result.Length > acceptableNumberLength)
                            {
                                result = result.Remove(acceptableNumberLength).Insert(indexOfDecimal, ".") /*+ length*/; ;
                            }
                            else
                            {
                                result = result.Insert(indexOfDecimal, ".") /*+ length*/;
                            }
                        }
                    }
                    else if (eToPowerOf > 0)
                    {
                        if (startsWithNeg)
                        {
                            if (result.Length > acceptableNumberLength)
                            {
                                result = "-" + result.Remove(acceptableNumberLength).Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                            else
                            {
                                result = "-" + result.Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                        }
                        else
                        {
                            if (result.Length > acceptableNumberLength)
                            {
                                result = result.Remove(acceptableNumberLength).Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                            else
                            {
                                result = result.Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                        }
                    }
                    else if (eToPowerOf < 0)
                    {
                        if (startsWithNeg)
                        {
                            if (result.Length > acceptableNumberLength)
                            {
                                result = "-" + result.Remove(acceptableNumberLength).Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                            else
                            {
                                result = "-" + result.Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                        }
                        else
                        {
                            if (result.Length > acceptableNumberLength)
                            {
                                result = result.Remove(acceptableNumberLength).Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                            else
                            {
                                result = result.Insert(i, ".").TrimStart('0').TrimEnd('0') + "E" + eToPowerOf;
                            }
                        }
                    }

                }
                return result;

            }

            return mathString;

        }
        //public string mathString_ToMathStringE(string mathString)
        //{
        //    int acceptableNumberLength = 7;
        //    string result = mathString;
        //    int indexOfDecimalE = mathString.IndexOf('.');

        //    if (indexOfDecimalE != -1 && (indexOfDecimalE >= acceptableNumberLength || result.Length >= acceptableNumberLength))
        //    {

        //        bool contrainsNeg = result.StartsWith("-");
        //        --indexOfDecimalE;
        //        if (!contrainsNeg)
        //        {
        //            int indexOfE = mathString.IndexOf("E+");

        //            if (indexOfE > -1)
        //            {
        //                result = mathString.Remove(acceptableNumberLength) + "E+" + mathString.Remove(0, indexOfE + 2);
        //            }
        //            else
        //            {
        //                result = mathString.Replace(".", "").Insert(1, ".");
        //                result = result.Remove(acceptableNumberLength) + "E-" + indexOfDecimalE;
        //            }
        //        }
        //        else if (contrainsNeg && result.Length - 1 >= acceptableNumberLength)
        //        {
        //            int indexOfE = mathString.IndexOf("E+");

        //            if (indexOfE > -1)
        //            {
        //                result = mathString.Remove(acceptableNumberLength /*+ 1*/) + "E+" + mathString.Remove(0, indexOfE + 2);
        //            }
        //            else
        //            {
        //                indexOfE = mathString.IndexOf("E-");
        //                if (indexOfE == -1)
        //                    indexOfDecimalE = 0;

        //                bool startsWith0 = false;
        //                result = mathString.Replace(".", "").Remove(0, 1).Insert(1, ".");

        //                while (result[1].Equals("0"))
        //                {
        //                    startsWith0 = true;
        //                    result = result.Remove(1, 1);
        //                    if (result.Count() == 0) break;
        //                    indexOfDecimalE++;
        //                }


        //                result = "-" + result.Remove(acceptableNumberLength /*+ 1*/) + "E-" + indexOfDecimalE;
        //            }
        //        }

        //    }

        //    //00001
        //    //10000
        //    else if (indexOfDecimalE == -1 && result.Length >= acceptableNumberLength)
        //    {
        //        bool contrainsNeg = result.StartsWith("-");
        //        bool startsWith0 = false;
        //        if (!contrainsNeg)
        //        {
        //            result = mathString.Replace(".", "").Insert(1, ".");
        //            if (result[0].Equals("0"))
        //            {
        //                while (result[0].Equals("0"))
        //                {
        //                    startsWith0 = true;
        //                    result = result.Remove(0, 1);
        //                    indexOfDecimalE++;
        //                }
        //            }
        //            else
        //            {
        //                indexOfDecimalE = mathString.Length - 1;
        //            }

        //            if (!startsWith0)
        //            {
        //                result = result.Remove(acceptableNumberLength) + "E+" + indexOfDecimalE;
        //            }
        //            else
        //            {
        //                result = result.Insert(0, "0");
        //                result = result.Remove(acceptableNumberLength + 1) + "E-" + indexOfDecimalE;
        //            }
        //        }
        //        else
        //        {
        //            result = mathString.Replace(".", "").Insert(2, ".");
        //            if (result[1].Equals("0"))
        //            {
        //                while (result[1].Equals("0"))
        //                {
        //                    startsWith0 = true;
        //                    result = result.Remove(1, 1);
        //                    indexOfDecimalE++;
        //                }

        //                result = result.Insert(0, "0");
        //                result = result.Remove(acceptableNumberLength + 1) + "E-" + indexOfDecimalE;
        //            }
        //            else
        //            {
        //                int indexOfE_P = mathString.IndexOf("E+");
        //                int indexOfE_N = mathString.IndexOf("E-");

        //                if (indexOfE_P > -1 || indexOfE_N > -1)
        //                {
        //                    if (indexOfE_P >= acceptableNumberLength || indexOfE_N >= acceptableNumberLength)
        //                    {
        //                        if (indexOfE_P > -1)
        //                            result = mathString.Remove(acceptableNumberLength) + "E+" + mathString.Remove(0, indexOfE_P + 2);
        //                        else if (indexOfE_N > -1)
        //                            result = mathString.Remove(acceptableNumberLength) + "E-" + mathString.Remove(0, indexOfE_N + 2);

        //                    }
        //                    else
        //                        result = mathString;
        //                }
        //                else
        //                {
        //                    indexOfDecimalE = mathString.Length - 2;
        //                    result = result.Remove(acceptableNumberLength) + "E+" + indexOfDecimalE;
        //                }
        //            }


        //        }
        //    }
        //    return result;
        //}
    }
}
