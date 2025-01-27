using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Perseverance_Calculator_1.Model.MVVM;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Input;
using System.Xml.XPath;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Globalization;
using System.ComponentModel.Design;
using CommunityToolkit.Common;
using System.Data;
//using Microsoft.Toolkit.Uwp.UI.Converters;


namespace Perseverance_Calculator_1.Controller
{
    internal partial class MathVue<T>
    {

        public string solveFormula(Formula formula, string formula_Str = null, bool assignRearrange_OnMainThread = true)
        {
            //tanSwitchCount = -1;

            string result = "";
            string formulaToSolve = "";
            if (formula != null)
                formulaToSolve = formula.formula;
            else
                formulaToSolve = formula_Str;
            bool containsError = false;
            //if (!string.IsNullOrWhiteSpace(formula_Str))
            if (!string.Equals(formula_Str, null, StringComparison.CurrentCulture))
            {
                formulaToSolve = formula_Str;

                //formulaToSolve = new StringVue().replaceFormulaFunction(formulaToSolve);
                formulaToSolve = removeNewLineAndSpacecs(formulaToSolve).Replace(']', ')').Replace('[', '(');
                formulaToSolve = new StringVue().replaceFormulaFunction(formulaToSolve);
                ////////////try
                ////////////{

                formulaToSolve = setVariable(formula, formulaToSolve);
                ////////////}
                ////////////catch
                ////////////{
                ////////////    containsError = true;
                ////////////    result = "Error";
                ////////////}
            }
            else
            {
                //formulaToSolve = new StringVue().replaceFormulaFunction(formulaToSolve);
                formulaToSolve = removeNewLineAndSpacecs(formula.formula).Replace(']', ')').Replace('[', '(');
                formulaToSolve = new StringVue().replaceFormulaFunction(formulaToSolve);
                //formulaToSolve = new StringVue().replaceFormulaFunction(formulaToSolve).result;
                //    //try
                //    //{
                formulaToSolve = setVariable(formula, formulaToSolve);
                //    //}
                //    //catch
                //    //{
                //    //    containsError = true;
                //    //    result = "Error";
                //    //}
            }

            //formulaToSolve = new StringVue().replaceFormulaFunction(formulaToSolve);
            if (!containsError)
            {
                if (!string.IsNullOrWhiteSpace(formulaToSolve) && !formulaToSolve.Equals("", StringComparison.CurrentCulture))
                {
                    //if (T_auto_F_Decimal_DoubleSolve)
                    //{
                    if (typeof(T).Equals(typeof(decimal)))
                    {
                        //try
                        //{
                        result = solveParenthesis_Functions(formulaToSolve, formula, false, false, assignRearrange_OnMainThread);
                        //}
                        //catch (DivideByZeroException)
                        //{
                        //    result = "Error:  Divided By Zero";
                        //}
                        //catch (OverflowException)
                        //{
                        //    try
                        //    {
                        if (result.Equals("Infinity"))
                            result = new MathVue<double>().solveFormula(formula, "", assignRearrange_OnMainThread);
                        //    }
                        //    catch (DivideByZeroException)
                        //    {
                        //        result = "Error:  Divided By Zero";
                        //    }
                        //    catch (OverflowException)
                        //    {
                        //        result = "Infinity";
                        //    }
                        //    catch
                        //    {
                        //        result = "Error";
                        //        //other error
                        //    }
                        //}
                        //catch (Exception e)
                        //{
                        //    if (e.Message.Equals("Asymptote Error"))
                        //        result = "Asymptote Error";
                        //    else if (e.Message.Equals("Positive Infinity Error"))
                        //    {
                        //        result = "Positive Infinity Error";
                        //    }
                        //    else if (e.Message.Equals("Negative Infinity Error"))
                        //    {
                        //        result = "Negative Infinity Error";
                        //    }
                        //    else if (e.Message.Contains("throw"))
                        //    {
                        //        result = e.Message;
                        //    }
                        //    else if (e.Message.Contains("Program Error"))
                        //    {
                        //        result = e.Message;
                        //    }
                        //    else
                        //        result = "Error";
                        //    //other error
                        //}
                    }
                    else
                    {
                        //////////try
                        //////////{
                        result = solveParenthesis_Functions(formulaToSolve, formula, false, false, assignRearrange_OnMainThread);
                        //////////}
                        //////////catch (DivideByZeroException)
                        //////////{
                        //////////    result = "Error:  Divided By Zero";
                        //////////}
                        //////////catch (OverflowException)
                        //////////{
                        //////////    result = "Infinity";
                        //////////}
                        //////////catch (Exception e)
                        //////////{
                        //////////    if (e.Message.Equals("Asymptote Error"))
                        //////////        result = "Asymptote Error";
                        //////////    else if (e.Message.Equals("Positive Infinity Error"))
                        //////////    {
                        //////////        result = "Positive Infinity Error";
                        //////////    }
                        //////////    else if (e.Message.Equals("Negative Infinity Error"))
                        //////////    {
                        //////////        result = "Negative Infinity Error";
                        //////////    }
                        //////////    else if (e.Message.Contains("throw"))
                        //////////    {
                        //////////        result = e.Message;
                        //////////    }
                        //////////    else if (e.Message.Contains("Program Error"))
                        //////////    {
                        //////////        result = e.Message;
                        //////////    }
                        //////////    else
                        //////////        result = "Error";
                        //////////    //other error
                        //////////}
                    }
                    //}
                    //else
                    //{

                    //    try
                    //    {
                    //        result = solveParenthesis_Functions(formula);
                    //    }
                    //    catch (DivideByZeroException)
                    //    {
                    //        result = "Error:  Divided By Zero";
                    //    }
                    //    catch (OverflowException)
                    //    {
                    //        result = "Infinity";
                    //    }
                    //}
                }
            }
            //return mathString_ToMathStringE(result, 28);
            //try
            //{
            //    return double.Parse(result).ToString();
            //}
            //catch
            //{
            return result;
            //}
        }

        //--------------------------------------------------------------getFunction_custom

        public bool isComma(char charToChk)
        {
            if (charToChk.Equals(','))
                return true;
            else return false;
        }
        public bool isOperator(char charToChk)
        {
            if (charToChk.Equals('+') ||
               charToChk.Equals('-') ||
               charToChk.Equals('^') ||
               charToChk.Equals('*') ||
               charToChk.Equals('%') ||
               charToChk.Equals('/')
               )
                return true;
            else return false;
        }
        public bool isParenthesis(char charToChk)
        {
            if (charToChk.Equals('(') ||
               charToChk.Equals(')') ||
               charToChk.Equals('[') ||
               charToChk.Equals(']')
               )
                return true;
            else return false;
        }

        public bool isEqualNumberOfParenthesis(string expression)
        {
            int count_L = 0;
            int count_R = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i].Equals('(') || expression[i].Equals('['))
                {
                    count_L++;
                }
                else if (expression[i].Equals(')') || expression[i].Equals(']'))
                {
                    count_R++;
                }
            }
            if (count_L - count_R == 0)
            {
                return true;
            }
            return false;
        }

        public bool isNumber(string strToChk)
        {
            double outNum = -1;
            return double.TryParse(strToChk, out outNum);
        }

        bool doesVarExist(ObservableCollection<VariableData> variableDatas, string varName_ToCheck)
        {
            foreach (VariableData data in variableDatas)
            {
                if (varName_ToCheck.Equals(data.name))
                    return true;
            }
            return false;
        }

        string removeNewLineAndSpacecs(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                while (str.LastIndexOf('\n') >= 0)
                {
                    str = str.Remove(str.LastIndexOf("\n"), 1);
                }
                while (str.LastIndexOf('\r') >= 0)
                {
                    str = str.Remove(str.LastIndexOf("\r"), 1);
                }
                while (str.LastIndexOf(' ') >= 0)
                {
                    str = str.Remove(str.LastIndexOf(" "), 1);
                }
                return str;
            }
            return "";
        }

        void setVariableDictionary(Formula formula)
        {
            if (formula != null)
            {
                formula.variableList_Dictionary.Clear();
                foreach (VariableData data in formula.variableList_Bind)
                {
                    formula.variableList_Dictionary.Add(data.name, data);
                }
            }
        }


        bool isPossibleVarNameToIgnore(string currentFunctionName, List<string> possibleVarNameToIgnore)
        {
            if (possibleVarNameToIgnore == null) return false;
            else
            {
                foreach (string s in possibleVarNameToIgnore)
                {
                    if (currentFunctionName.Equals(s))
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        //private bool checkIsIndexWithinNestedSingleFunction(List<(bool isNested, int closeParIndex)> isNestedSingleFunction, int currentIndex) {
        //    int i = 0;
        //    while(isNestedSingleFunction.Count>0)
        //    {
        //        if (currentIndex <= isNestedSingleFunction[i].closeParIndex)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            isNestedSingleFunction.RemoveAt(i);
        //        }
        //    }
        //    return false;
        //}

        private string setVariable(Formula formula_Obj, string rearrangedFormula = null, Dictionary<string, string> commaSeparated_List = null, bool solveFunction = false, bool isSingleVar = false, List<string> possibleVarNameToIgnore = null, bool setVarTo_isNotIgnoreColOnly = false)
        {
            setVariableDictionary(formula_Obj);

            //List<(bool isNested, int closeParIndex)> isNestedSingleFunction = new List<(bool isNested, int closeParIndex)>();

            string currentFunctionName = "";
            int currentFunctionName_Column = -1;
            int currentFunctionName_Index = -1;
            int currentFunctionName_MaxColumn = -1;
            //List<(string functionName, int openParIndex, int currentColumn, int closeParIndex, int currentFunctionNumberOfColumn)> functionName = new List<(string, int, int, int, int)>();

            List<(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)> functionName =
                new List<(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)>();

            string formula = "";

            if (!string.Equals(rearrangedFormula, null, StringComparison.CurrentCulture))
            {
                formula = rearrangedFormula;
            }
            else
            {
                if (formula_Obj != null)
                    formula = formula_Obj.formula;
            }


            string possibleVariable = "";

            formula = removeNewLineAndSpacecs(formula).Replace(']', ')').Replace('[', '(');

            //Dictionary<string, List<int>> splitStr = splitStr_Ignore_SolveFunctions_DefaultMultiVariable(formula);
            //(bool, List<string>, List<int>) isIgnoreFunctionCol = (false,null,null);

            //isEqualToVariableColumn_InList(1, isIgnoreFunctionCol.Item3);
            //int currentIndexOf_CommaSeparatedList = 0;

            if (!string.IsNullOrWhiteSpace(formula))
            {
                //int functionName_Count = 0;
                //bool setVarTo_1stNestedFuntionOnly_CanSetVar = true;
                //bool shouldContinue = false;
                int formulaLength = formula.Length;
                for (int i = 0; i < formulaLength; i++)
                {
                    int ignoreSelectFunction_index = getIgnoreInCurrentColumn(functionName, i).selectedFunctionName_Index;
                    if (currentFunctionName_Index == -1)
                        currentFunctionName_Index = i;
                    //if (setVarTo_1stNestedFuntionOnly && functionName.Count > 0 && functionName_Count!= functionName.Count)
                    //{

                    //    setVarTo_1stNestedFuntionOnly_CanSetVar = true;
                    //    functionName_Count = functionName.Count;
                    //    for (int q = 0; q < functionName.Count-1; i++)
                    //    {
                    //        if(functionName[functionName.Count - 1].closeParIndex < functionName[q].closeParIndex)
                    //        {
                    //            setVarTo_1stNestedFuntionOnly_CanSetVar = false;
                    //            break;
                    //        }
                    //    }

                    //}

                    //if (string.IsNullOrWhiteSpace(currentFunctionName))
                    //{
                    //    shouldContinue = true;
                    //}
                    //if (setVarTo_NestedFuntionOnly)
                    //{

                    //}

                    if (

                        (i == formulaLength - 1 && !isParenthesis(formula[i]) && !isOperator(formula[i]))

                        )
                    {

                        possibleVariable += formula[i];
                        //if (!doesVarExist(variableDataList, possibleVariable))
                        //{
                        if (!string.IsNullOrWhiteSpace(possibleVariable) && !isNumber(possibleVariable))
                        {
                            //if (is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item1 &&
                            //    isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item2) &&
                            //    isEqualToVariableColumn_InList(currentFunctionName_Column, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item3)) { }

                            if (isIgnoreColumn(functionName, possibleVariable, currentFunctionName_Column, i) || isPossibleVarNameToIgnore(possibleVariable, possibleVarNameToIgnore)) { }
                            else if (commaSeparated_List != null && !solveFunction && (commaSeparated_List.ContainsKey(possibleVariable) || isSingleVar))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1), possibleVariable.Length);
                                if (isSingleVar)
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1), commaSeparated_List.ElementAt(0).Value);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List.ElementAt(0).Value.ToString().Length);
                                }
                                else
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1), commaSeparated_List[possibleVariable]);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List[possibleVariable].Length);
                                }
                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;

                                //currentIndexOf_CommaSeparatedList++;
                            }
                            else if (formula_Obj != null && formula_Obj.variableList_Dictionary.ContainsKey(possibleVariable))
                            {

                                formula = formula.Remove(i - (possibleVariable.Length - 1), possibleVariable.Length);
                                if (string.IsNullOrWhiteSpace(formula_Obj.variableList_Dictionary[possibleVariable].value))
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1), "");
                                    i = i - (possibleVariable.Length) + 0;
                                }
                                else
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1), formula_Obj.variableList_Dictionary[possibleVariable].value);
                                    i = i - (possibleVariable.Length) + (formula_Obj.variableList_Dictionary[possibleVariable].value.Length);
                                }

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (DataDataCollection_Project_MVVM.dataValueList.ContainsKey(possibleVariable))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1), possibleVariable.Length);
                                formula = formula.Insert(i - (possibleVariable.Length - 1), DataDataCollection_Project_MVVM.dataValueList[possibleVariable]);
                                i = i - (possibleVariable.Length) + (DataDataCollection_Project_MVVM.dataValueList[possibleVariable].ToString().Length);

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable) &&
                                !FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].use.Contains('('))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1), possibleVariable.Length);
                                formula = formula.Insert(i - (possibleVariable.Length - 1), FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].formulaObj.rearrangedFormula);
                                i = i - (possibleVariable.Length) + ((FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].formulaObj.rearrangedFormula).Length);

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (commaSeparated_List != null && (commaSeparated_List.ContainsKey(possibleVariable) || isSingleVar))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1), possibleVariable.Length);
                                if (isSingleVar)
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1), commaSeparated_List.ElementAt(0).Value);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List.ElementAt(0).Value.ToString().Length);
                                }
                                else
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1), commaSeparated_List[possibleVariable]);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List[possibleVariable].Length);
                                }

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            //variableDataList.Add(new VariableData() { name = possibleVariable });
                            possibleVariable = "";
                        }
                        else if (isNumber(possibleVariable))
                        {
                            possibleVariable = "";
                        }

                        //}
                        //else { possibleVariable = ""; }

                    }
                    else if (!isOperator(formula[i]) && !isParenthesis(formula[i]) && !isComma(formula[i]))
                    {
                        possibleVariable += formula[i];
                    }
                    else if (isOperator(formula[i]) || isComma(formula[i]))
                    {
                        //if (!doesVarExist(variableDataList, possibleVariable))
                        //{
                        if (!string.IsNullOrWhiteSpace(possibleVariable) && !isNumber(possibleVariable))
                        {
                            //if (is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item1 &&
                            //    isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item2) &&
                            //    isEqualToVariableColumn_InList(currentFunctionName_Column, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item3)) { }

                            if (isIgnoreColumn(functionName, possibleVariable, currentFunctionName_Column, i) || isPossibleVarNameToIgnore(possibleVariable, possibleVarNameToIgnore)) { }
                            else if (commaSeparated_List != null && !solveFunction && (commaSeparated_List.ContainsKey(possibleVariable) || isSingleVar))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                if (isSingleVar)
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List.ElementAt(0).Value);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List.ElementAt(0).Value.ToString().Length);
                                }
                                else
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List[possibleVariable]);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List[possibleVariable].Length);
                                }
                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (formula_Obj != null && formula_Obj.variableList_Dictionary.ContainsKey(possibleVariable))
                            {

                                formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                if (string.IsNullOrWhiteSpace(formula_Obj.variableList_Dictionary[possibleVariable].value))
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, "");
                                    i = i - (possibleVariable.Length) + 0;
                                }
                                else
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, formula_Obj.variableList_Dictionary[possibleVariable].value);
                                    i = i - (possibleVariable.Length) + (formula_Obj.variableList_Dictionary[possibleVariable].value.Length);
                                }

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (DataDataCollection_Project_MVVM.dataValueList.ContainsKey(possibleVariable))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, DataDataCollection_Project_MVVM.dataValueList[possibleVariable]);
                                i = i - (possibleVariable.Length) + (DataDataCollection_Project_MVVM.dataValueList[possibleVariable].ToString().Length);

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable) &&
                                 !FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].use.Contains('('))
                            {
                                formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].formulaObj.rearrangedFormula);
                                i = i - (possibleVariable.Length) + ((FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].formulaObj.rearrangedFormula).Length);

                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            else if (commaSeparated_List != null && (commaSeparated_List.ContainsKey(possibleVariable) || isSingleVar))
                            {

                                formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                if (isSingleVar)
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List.ElementAt(0).Value);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List.ElementAt(0).Value.ToString().Length);
                                }
                                else
                                {
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List[possibleVariable]);
                                    i = i - (possibleVariable.Length) + (commaSeparated_List[possibleVariable].Length);
                                }
                                if (ignoreSelectFunction_index != -1)
                                {
                                    functionName[ignoreSelectFunction_index] =
                                        new Tuple<string, int, int, int, int>(
                                            functionName[ignoreSelectFunction_index].Item1,
                                            functionName[ignoreSelectFunction_index].Item2,
                                            functionName[ignoreSelectFunction_index].Item3,
                                            functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                        ).ToValueTuple();

                                }
                                formulaLength = formula.Length;
                            }
                            //variableDataList.Add(new VariableData() { name = possibleVariable });
                            possibleVariable = "";
                        }
                        else if (isNumber(possibleVariable))
                        {
                            possibleVariable = "";
                        }
                        if (isComma(formula[i]) && ignoreSelectFunction_index != -1)
                        {
                            functionName[ignoreSelectFunction_index] =
                                new Tuple<string, int, int, int, int>(
                                    functionName[ignoreSelectFunction_index].Item1,
                                    functionName[ignoreSelectFunction_index].Item2,
                                    functionName[ignoreSelectFunction_index].Item3 + 1,
                                    functionName[ignoreSelectFunction_index].Item4,
                                                functionName[ignoreSelectFunction_index].Item5
                                ).ToValueTuple();
                            currentFunctionName_Column = functionName[ignoreSelectFunction_index].Item3;
                        }
                        //}
                        //else { possibleVariable = ""; }
                    }
                    else if (isParenthesis(formula[i]))
                    {
                        if ((formula[i].Equals(')') || formula[i].Equals(']')) && !isNumber(possibleVariable))
                        {
                            if (!string.IsNullOrWhiteSpace(possibleVariable))
                            {
                                //if (is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item1 &&
                                //    isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item2) &&
                                //    isEqualToVariableColumn_InList(currentFunctionName_Column, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item3)) { }
                                if (isIgnoreColumn(functionName, possibleVariable, currentFunctionName_Column, i) || isPossibleVarNameToIgnore(possibleVariable, possibleVarNameToIgnore)) { }
                                else if (commaSeparated_List != null && !solveFunction && (commaSeparated_List.ContainsKey(possibleVariable) || isSingleVar))
                                {

                                    formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                    if (isSingleVar)
                                    {
                                        formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List.ElementAt(0).Value);
                                        i = i - (possibleVariable.Length) + (commaSeparated_List.ElementAt(0).Value.ToString().Length);
                                    }
                                    else
                                    {
                                        formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List[possibleVariable]);
                                        i = i - (possibleVariable.Length) + (commaSeparated_List[possibleVariable].Length);
                                    }
                                    if (ignoreSelectFunction_index != -1)
                                    {
                                        functionName[ignoreSelectFunction_index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[ignoreSelectFunction_index].Item1,
                                                functionName[ignoreSelectFunction_index].Item2,
                                                functionName[ignoreSelectFunction_index].Item3,
                                                functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                            ).ToValueTuple();

                                    }
                                    formulaLength = formula.Length;
                                }
                                else if (formula_Obj != null && formula_Obj.variableList_Dictionary.ContainsKey(possibleVariable))
                                {

                                    formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                    if (string.IsNullOrWhiteSpace(formula_Obj.variableList_Dictionary[possibleVariable].value))
                                    {
                                        formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, "");
                                        i = i - (possibleVariable.Length) + 0;
                                    }
                                    else
                                    {
                                        formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, formula_Obj.variableList_Dictionary[possibleVariable].value);
                                        i = i - (possibleVariable.Length) + (formula_Obj.variableList_Dictionary[possibleVariable].value.Length);
                                    }

                                    if (ignoreSelectFunction_index != -1)
                                    {
                                        functionName[ignoreSelectFunction_index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[ignoreSelectFunction_index].Item1,
                                                functionName[ignoreSelectFunction_index].Item2,
                                                functionName[ignoreSelectFunction_index].Item3,
                                                functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                            ).ToValueTuple();

                                    }
                                    formulaLength = formula.Length;
                                }
                                else if (DataDataCollection_Project_MVVM.dataValueList.ContainsKey(possibleVariable))
                                {
                                    formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, DataDataCollection_Project_MVVM.dataValueList[possibleVariable]);
                                    i = i - (possibleVariable.Length) + (DataDataCollection_Project_MVVM.dataValueList[possibleVariable].ToString().Length);

                                    if (ignoreSelectFunction_index != -1)
                                    {
                                        functionName[ignoreSelectFunction_index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[ignoreSelectFunction_index].Item1,
                                                functionName[ignoreSelectFunction_index].Item2,
                                                functionName[ignoreSelectFunction_index].Item3,
                                                functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                            ).ToValueTuple();

                                    }
                                    formulaLength = formula.Length;
                                }
                                else if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable) &&
                                 !FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].use.Contains('('))
                                {
                                    formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                    formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].formulaObj.rearrangedFormula);

                                    i = i - (possibleVariable.Length) + ((FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].formulaObj.rearrangedFormula).Length);

                                    if (ignoreSelectFunction_index != -1)
                                    {
                                        functionName[ignoreSelectFunction_index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[ignoreSelectFunction_index].Item1,
                                                functionName[ignoreSelectFunction_index].Item2,
                                                functionName[ignoreSelectFunction_index].Item3,
                                                functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                            ).ToValueTuple();

                                    }
                                    formulaLength = formula.Length;
                                }
                                else if (commaSeparated_List != null && (commaSeparated_List.ContainsKey(possibleVariable) || isSingleVar))
                                {

                                    formula = formula.Remove(i - (possibleVariable.Length - 1) - 1, possibleVariable.Length);
                                    if (isSingleVar)
                                    {
                                        formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List.ElementAt(0).Value);
                                        i = i - (possibleVariable.Length) + (commaSeparated_List.ElementAt(0).Value.ToString().Length);
                                    }
                                    else
                                    {
                                        formula = formula.Insert(i - (possibleVariable.Length - 1) - 1, commaSeparated_List[possibleVariable]);
                                        i = i - (possibleVariable.Length) + (commaSeparated_List[possibleVariable].Length);
                                    }
                                    if (ignoreSelectFunction_index != -1)
                                    {
                                        functionName[ignoreSelectFunction_index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[ignoreSelectFunction_index].Item1,
                                                functionName[ignoreSelectFunction_index].Item2,
                                                functionName[ignoreSelectFunction_index].Item3,
                                                functionName[ignoreSelectFunction_index].Item4 + formula.Length - formulaLength,
                                                functionName[ignoreSelectFunction_index].Item5
                                            ).ToValueTuple();

                                    }
                                    formulaLength = formula.Length;
                                }
                            }

                            //if (new StringVue().indexOf(formula, ")", currentFunctionName_Index, true) <= i)
                            if (getIgnoreInCurrentColumn(functionName, i).selectedFunctionName_Index == -1)
                            {
                                currentFunctionName_Index = i;
                                currentFunctionName = "";
                                //currentFunctionName_Column = -1;
                                //functionName.Clear
                            }
                        }
                        else
                        {
                            //int closeParIndex = new StringVue().indexOf(formula, ")", currentFunctionName_Index, true);
                            if (((formula[i].Equals('(') || formula[i].Equals('['))) /*&& closeParIndex >= i*/)
                            {

                                if (!string.IsNullOrWhiteSpace(possibleVariable) && is_SolveFunctions_DefaultMultiVariable(possibleVariable).Item1)
                                {
                                    int closeParIndex = new StringVue().indexOf(formula, ")", i, true);
                                    currentFunctionName_Index = i;
                                    currentFunctionName = possibleVariable;
                                    currentFunctionName_Column = 0;
                                    currentFunctionName_MaxColumn = is_SolveFunctions_DefaultMultiVariable(possibleVariable).totalNumberOfColumns;
                                    functionName.Add((currentFunctionName, currentFunctionName_Index, currentFunctionName_Column, closeParIndex, currentFunctionName_MaxColumn));
                                }
                                //else if (!string.IsNullOrWhiteSpace(possibleVariable))
                                //{
                                //    isNestedSingleFunction.Add((true, new StringVue().indexOf(formula, ")", i, true)));
                                //}
                            }
                        }
                        //if (i - 1 >= 0 && isParenthesis(formula[i]) && formula[i].Equals('(') && !formula[i - 1].Equals('('))
                        //{
                        //    currentFunctionName = possibleVariable;
                        //}

                        possibleVariable = "";
                    }

                    else if (isOperator(formula[i]) || isComma(formula[i]))
                    {
                        possibleVariable = "";
                    }
                }

            }
            return formula;
        }
        private (int selectedFunctionName_Index, int openPar, int closePar, int currentFunctionName_Column, int currentFunctionName_NumberOfColumn) getIgnoreInCurrentColumn(List<(string, int, int, int, int)> functionNameIgnoreCol, int currentIndex)
        {

            (int, int, int, int, int) selectedIndex = (-1, -1, -1, -1, -1);
            for (int i = functionNameIgnoreCol.Count - 1; i >= 0; i--)
            {
                //item2 == left (
                //item4 == left )
                if (currentIndex >= functionNameIgnoreCol[i].Item2 && currentIndex <= functionNameIgnoreCol[i].Item4)
                {
                    if (selectedIndex.Item2 < functionNameIgnoreCol[i].Item2)
                        selectedIndex = (i, functionNameIgnoreCol[i].Item2, functionNameIgnoreCol[i].Item4, functionNameIgnoreCol[i].Item3, functionNameIgnoreCol[i].Item5);
                }
            }

            return selectedIndex;
        }
        private bool isIgnoreColumn(List<(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)> functionNameIgnoreCol, string possibleVariable, int currentColumnToIgnore, int currentIndex)
        {

            //List<(string, int, int, int)> functionName = new List<(string, int, int, int)>();
            //(bool, List<string> variablesToIgnore, List<int> columnsToIgnore, int totalNumberOfColumns, bool ignoreAllVar) is_SolveFunctions_DefaultMultiVariable
            foreach (var v in functionNameIgnoreCol)
            {
                if (is_SolveFunctions_DefaultMultiVariable(v.Item1).Item1 &&
                    (isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(v.Item1).Item2) ||
                    is_SolveFunctions_DefaultMultiVariable(v.Item1).ignoreAllVar) &&
                    isEqualToVariableColumn_InList(currentColumnToIgnore, is_SolveFunctions_DefaultMultiVariable(v.Item1).Item3) &&
                    (currentIndex <= v.closeParIndex)) { return true; }

            }
            return false;
        }

        private bool isStringEqualToIgnoreIf_ContainList(string str, List<string> list)
        {
            foreach (var v in list)
            {
                if (str.Contains(v))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isStringEqualToIgnoreIf_EqualsList(string str, List<string> list)
        {
            foreach (var v in list)
            {
                if (str.Equals(v))
                {
                    return true;
                }
            }
            return false;
        }

        public ObservableCollection<VariableData> getVariables(string formula, Formula formula_Obj, Dictionary<string, VariableData> variableDataDictionary)
        {

            List<string> varToIgnoreIfContain = new List<string>() { "throw" };
            List<string> varToIgnoreIfEqual = new List<string>() { "true", "false" };

            ObservableCollection<VariableData> variableDataList = new ObservableCollection<VariableData>();
            string possibleVariable = "";
            //====for multivariable
            int currentFunctionName_Index = -1;
            int currentFunction_Index = -1;
            string currentFunctionName = "";
            int currentFunctionName_Column = -1;
            int currentFunctionName_MaxColumn = -1;

            List<(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)> functionName =
                new List<(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)>();
            //====

            formula = removeNewLineAndSpacecs(formula).Replace(']', ')').Replace('[', '(');

            if (!string.IsNullOrWhiteSpace(formula))
            {
                int formulaLength = formula.Length;
                for (int i = 0; i < formulaLength; i++)
                {
                    if (currentFunctionName_Index == -1)
                        currentFunctionName_Index = i;

                    if (i == formulaLength - 1 && !isParenthesis(formula[i]) && !isOperator(formula[i]))
                    {
                        possibleVariable += formula[i];
                        if (isStringEqualToIgnoreIf_ContainList(possibleVariable, varToIgnoreIfContain)) { possibleVariable = ""; }
                        if (isStringEqualToIgnoreIf_EqualsList(possibleVariable, varToIgnoreIfEqual)) { possibleVariable = ""; }
                        else if (!doesVarExist(variableDataList, possibleVariable))
                        {
                            if (!string.IsNullOrWhiteSpace(possibleVariable) && !isNumber(possibleVariable))
                            {
                                //if (is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item1 &&
                                //    isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item2) &&
                                //    isEqualToVariableColumn_InList(currentFunctionColumn, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item3)) { }

                                if (isIgnoreColumn(functionName, possibleVariable, currentFunctionName_Column, i)) { }
                                else if (!DataDataCollection_Project_MVVM.dataValueList.ContainsKey(possibleVariable) &&
                                    !FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable))
                                {
                                    if (!variableDataDictionary.ContainsKey(possibleVariable))
                                        variableDataList.Add(new VariableData() { name = possibleVariable, value = possibleVariable });
                                    else
                                        variableDataList.Add(new VariableData()
                                        {
                                            name = variableDataDictionary[possibleVariable].name,
                                            value = variableDataDictionary[possibleVariable].value,
                                            description =
                                            variableDataDictionary[possibleVariable].description
                                        });

                                }
                                else if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable) &&
                                 FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].use.Contains('('))
                                {
                                    if (!variableDataDictionary.ContainsKey(possibleVariable))
                                        variableDataList.Add(new VariableData() { name = possibleVariable, value = possibleVariable });
                                    else
                                        variableDataList.Add(new VariableData()
                                        {
                                            name = variableDataDictionary[possibleVariable].name,
                                            value = variableDataDictionary[possibleVariable].value,
                                            description =
                                            variableDataDictionary[possibleVariable].description
                                        });
                                }
                                possibleVariable = "";
                            }
                            else if (isNumber(possibleVariable))
                            {
                                possibleVariable = "";
                            }

                        }
                        else { possibleVariable = ""; }

                    }
                    else if (!isOperator(formula[i]) && !isParenthesis(formula[i]) && !isComma(formula[i]))
                    {
                        possibleVariable += formula[i];
                    }
                    else if (isOperator(formula[i]) || isComma(formula[i]))
                    {
                        if (!doesVarExist(variableDataList, possibleVariable))
                        {
                            if (!string.IsNullOrWhiteSpace(possibleVariable) && !isNumber(possibleVariable))
                            {
                                //if (is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item1 &&
                                //    isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item2) &&
                                //    isEqualToVariableColumn_InList(currentFunctionColumn, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item3)) { }

                                if (isStringEqualToIgnoreIf_ContainList(possibleVariable, varToIgnoreIfContain)) { possibleVariable = ""; }
                                else if (isStringEqualToIgnoreIf_EqualsList(possibleVariable, varToIgnoreIfEqual)) { possibleVariable = ""; }

                                else if (isIgnoreColumn(functionName, possibleVariable, currentFunctionName_Column, i)) { }
                                else if (!DataDataCollection_Project_MVVM.dataValueList.ContainsKey(possibleVariable) &&
                                    !FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable))
                                {
                                    if (!variableDataDictionary.ContainsKey(possibleVariable))
                                        variableDataList.Add(new VariableData() { name = possibleVariable, value = possibleVariable });
                                    else
                                        variableDataList.Add(new VariableData()
                                        {
                                            name = variableDataDictionary[possibleVariable].name,
                                            value = variableDataDictionary[possibleVariable].value,
                                            description =
                                            variableDataDictionary[possibleVariable].description
                                        });

                                }
                                else if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable) &&
                                 FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].use.Contains('('))
                                {
                                    if (!variableDataDictionary.ContainsKey(possibleVariable))
                                        variableDataList.Add(new VariableData() { name = possibleVariable, value = possibleVariable });
                                    else
                                        variableDataList.Add(new VariableData()
                                        {
                                            name = variableDataDictionary[possibleVariable].name,
                                            value = variableDataDictionary[possibleVariable].value,
                                            description =
                                            variableDataDictionary[possibleVariable].description
                                        });
                                }
                                possibleVariable = "";
                            }
                            else if (isNumber(possibleVariable))
                            {
                                possibleVariable = "";
                            }
                        }
                        else { possibleVariable = ""; }

                        if (isComma(formula[i]))
                        {

                            currentFunctionName_Column++;

                            //////////////////////////////////////////////////////////////////if (currentFunction_Index >= 0 && functionName.Count >= currentFunction_Index)
                            //////////////////////////////////////////////////////////////////{
                            //////////////////////////////////////////////////////////////////    //(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)
                            //////////////////////////////////////////////////////////////////    functionName[currentFunction_Index] =
                            //////////////////////////////////////////////////////////////////        new Tuple<string, int, int, int, int>(
                            //////////////////////////////////////////////////////////////////            functionName[currentFunction_Index].currentFunctionName,
                            //////////////////////////////////////////////////////////////////            functionName[currentFunction_Index].currentFunctionName_Index,
                            //////////////////////////////////////////////////////////////////            currentFunctionName_Column,
                            //////////////////////////////////////////////////////////////////            functionName[currentFunction_Index].closeParIndex,
                            //////////////////////////////////////////////////////////////////            functionName[currentFunction_Index].currentFunctionName_MaxColumn
                            //////////////////////////////////////////////////////////////////            ).ToValueTuple();

                            //////////////////////////////////////////////////////////////////}

                        }
                        if (currentFunction_Index >= 0 && functionName.Count >= currentFunction_Index)
                        {
                            //&& functionName[functionName.Count - 1].closeParIndex - i <= 0
                            if (currentFunctionName_Column + 1 > functionName[currentFunction_Index].currentFunctionName_MaxColumn)
                            {

                                if (currentFunction_Index - 1 >= 0)
                                {
                                    functionName.RemoveAt(currentFunction_Index);
                                    currentFunction_Index--;
                                    if (currentFunction_Index >= 0 && functionName.Count > 0)
                                    {
                                        //if (currentFunctionName_Column + 1 > functionName[currentFunction_Index].currentFunctionName_MaxColumn)
                                        //{
                                        //    currentFunctionName_Column = functionName[currentFunction_Index].currentFunctionName_Column + 1;
                                        //}
                                        //else
                                        currentFunctionName_Column = functionName[currentFunction_Index].currentFunctionName_Column;
                                        //(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)
                                        functionName[currentFunction_Index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[currentFunction_Index].currentFunctionName,
                                                functionName[currentFunction_Index].currentFunctionName_Index,
                                                currentFunctionName_Column,
                                                functionName[currentFunction_Index].closeParIndex,
                                                functionName[currentFunction_Index].currentFunctionName_MaxColumn
                                                ).ToValueTuple();
                                        //currentFunctionName_Column;
                                    }
                                }
                                else if (i - functionName[functionName.Count - 1].closeParIndex <= 0)
                                {
                                    currentFunctionName_Column--;
                                }
                            }
                            //else if (isComma(formula[i]))
                            //{
                            //    //if (currentFunction_Index >= 0 && functionName.Count >= currentFunction_Index)
                            //    //{
                            //    //(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)
                            //    functionName[currentFunction_Index] =
                            //        new Tuple<string, int, int, int, int>(
                            //            functionName[currentFunction_Index].currentFunctionName,
                            //            functionName[currentFunction_Index].currentFunctionName_Index,
                            //            currentFunctionName_Column,
                            //            functionName[currentFunction_Index].closeParIndex,
                            //            functionName[currentFunction_Index].currentFunctionName_MaxColumn
                            //            ).ToValueTuple();

                            //    //}

                            //}

                        }
                    }
                    else if (isParenthesis(formula[i]))
                    {
                        if ((formula[i].Equals(')') || formula[i].Equals(']')) && !isNumber(possibleVariable))
                        {
                            if (!string.IsNullOrWhiteSpace(possibleVariable))
                            {
                                if (!doesVarExist(variableDataList, possibleVariable))
                                {
                                    //if (is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item1 &&
                                    //    isEqualToVariable_InList(possibleVariable, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item2) &&
                                    //    isEqualToVariableColumn_InList(currentFunctionColumn, is_SolveFunctions_DefaultMultiVariable(currentFunctionName).Item3)) { 

                                    //}
                                    if (isStringEqualToIgnoreIf_ContainList(possibleVariable, varToIgnoreIfContain)) { possibleVariable = ""; }
                                    else if (isStringEqualToIgnoreIf_EqualsList(possibleVariable, varToIgnoreIfEqual)) { possibleVariable = ""; }

                                    else if (isIgnoreColumn(functionName, possibleVariable, currentFunctionName_Column, i)) { }
                                    else if (!DataDataCollection_Project_MVVM.dataValueList.ContainsKey(possibleVariable) &&
                                    !FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable))
                                    {
                                        if (!variableDataDictionary.ContainsKey(possibleVariable))
                                            variableDataList.Add(new VariableData() { name = possibleVariable, value = possibleVariable });
                                        else
                                            variableDataList.Add(new VariableData()
                                            {
                                                name = variableDataDictionary[possibleVariable].name,
                                                value = variableDataDictionary[possibleVariable].value,
                                                description =
                                                variableDataDictionary[possibleVariable].description
                                            });

                                    }
                                    else if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(possibleVariable) &&
                                 FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[possibleVariable].use.Contains('('))
                                    {
                                        if (!variableDataDictionary.ContainsKey(possibleVariable))
                                            variableDataList.Add(new VariableData() { name = possibleVariable, value = possibleVariable });
                                        else
                                            variableDataList.Add(new VariableData()
                                            {
                                                name = variableDataDictionary[possibleVariable].name,
                                                value = variableDataDictionary[possibleVariable].value,
                                                description =
                                                variableDataDictionary[possibleVariable].description
                                            });
                                    }
                                }
                            }

                            //if (new StringVue().indexOf(formula, ")", currentFunctionName_Index, true) <= i)
                            //{
                            //    currentFunctionName_Index = i;
                            //    currentFunctionName = "";
                            //    currentFunctionColumn = -1;
                            //}

                            //if (new StringVue().indexOf(formula, ")", currentFunctionName_Index, true) <= i)
                            if (getIgnoreInCurrentColumn(functionName, i).selectedFunctionName_Index == -1)
                            {
                                currentFunctionName_Index = i;
                                currentFunctionName = "";
                                //currentFunctionName_Column = -1;
                                //functionName.Clear
                            }
                        }
                        else
                        {
                            //if (((formula[i].Equals('(') || formula[i].Equals('['))) && new StringVue().indexOf(formula, ")", currentFunctionName_Index, true) >= i)
                            //{
                            //    currentFunctionName_Index = i;
                            //    currentFunctionName = possibleVariable;
                            //    currentFunctionColumn = 0;
                            //}
                            //int closeParIndex = new StringVue().indexOf(formula, ")", currentFunctionName_Index, true);
                            if (((formula[i].Equals('(') || formula[i].Equals('['))) /*&& closeParIndex >= i*/)
                            {
                                if (!string.IsNullOrWhiteSpace(possibleVariable) && is_SolveFunctions_DefaultMultiVariable(possibleVariable).Item1)
                                {
                                    if (currentFunction_Index >= 0 && functionName.Count >= currentFunction_Index)
                                    {
                                        //if (currentFunction_Index >= 0 && functionName.Count >= currentFunction_Index)
                                        //{
                                        //(string currentFunctionName, int currentFunctionName_Index, int currentFunctionName_Column, int closeParIndex, int currentFunctionName_MaxColumn)
                                        functionName[currentFunction_Index] =
                                            new Tuple<string, int, int, int, int>(
                                                functionName[currentFunction_Index].currentFunctionName,
                                                functionName[currentFunction_Index].currentFunctionName_Index,
                                                currentFunctionName_Column,
                                                functionName[currentFunction_Index].closeParIndex,
                                                functionName[currentFunction_Index].currentFunctionName_MaxColumn
                                                ).ToValueTuple();

                                        //}

                                    }
                                    currentFunction_Index += 1;
                                    int closeParIndex = new StringVue().indexOf(formula, ")", i, true);
                                    currentFunctionName_Index = i;
                                    currentFunctionName = possibleVariable;
                                    currentFunctionName_Column = 0;
                                    currentFunctionName_MaxColumn = is_SolveFunctions_DefaultMultiVariable(possibleVariable).totalNumberOfColumns;
                                    functionName.Add((currentFunctionName, currentFunctionName_Index, currentFunctionName_Column, closeParIndex, currentFunctionName_MaxColumn));
                                }
                            }
                            //currentFunctionName = possibleVariable;
                            //currentFunctionColumn = 0;
                        }
                        possibleVariable = "";
                    }

                }

            }
            return variableDataList;
        }
        //private string rearrangeFormula(string formula)
        //{
        //    return formula;
        //}




        private bool isErrorSkip(string result)
        {
            if (result.Equals("Error") ||
                result.Equals("Error:  Divided By Zero") ||
                result.Equals("Asymptote Error") ||
                result.Equals("throw") ||
                result.Equals("Program Error") ||
                //result.Equals(double.PositiveInfinity.ToString()) ||
                result.Equals("Infinity"))
                return true;
            return false;
        }

        string getThrowString(string result)
        {
            string resultStr = "";
            int indexOfThrow = result.IndexOf("throw");
            if (indexOfThrow == -1) return "throw";
            for (int i = indexOfThrow; i < result.Length; i++)
            {
                if (isComma(result[i]) ||
                    isParenthesis(result[i]) ||
                    isOperator(result[i]))
                    return resultStr;
                resultStr += result[i];
            }
            return resultStr;

        }
        private (bool isError, string errorString) containsErrorSkip(string result)
        {
            (bool isError, string errorString) res = (false, result);
            if (result.Contains("Error:  Divided By Zero"))
            {
                res = (true, "Error:  Divided By Zero");
            }
            else if (result.Contains("Program Error"))
            {
                res = (true, "Program Error");
            }
            else if (result.Contains("Asymptote Error"))
            {
                res = (true, "Asymptote Error");

            }
            else if (result.Contains("Error"))
            {

                res = (true, "Error");
            }
            else if (result.Contains("throw"))
            {

                res = (true, getThrowString(result));
            }
            else if (result.Contains("Infinity"))
            {

                res = (true, "Infinity");
            }
            return res;
        }

        //--------------------------------------------------------------Calculation
        //--------------------------------------------------------------Calculation
        //--------------------------------------------------------------Calculation
        //--------------------------------------------------------------Calculation
        //TODO: number with E eg. 123E10
        private string parseAdd(string equation, ref int index, int eqLength, string result = "0")
        {
            string operand1 = "";

            if (index == 0)
            {
                operand1 = getIsNumber(equation, ref index, eqLength);
                result = operand1;
            }
            while (index < eqLength && equation[index].Equals('+') && !string.IsNullOrWhiteSpace(result))
            {
                index++;
                string operand2 = getIsNumber(equation, ref index, eqLength);
                while (index < eqLength && (equation[index].Equals('*') || equation[index].Equals('/') || equation[index].Equals('^') || equation[index].Equals('%')))
                {
                    if (equation[index].Equals('^'))
                        operand2 = parseExponent(equation, ref index, eqLength, operand2);
                    else if (equation[index].Equals('%'))
                        operand2 = parseModulus(equation, ref index, eqLength, operand2);
                    else if (equation[index].Equals('*'))
                        operand2 = parseMultiply(equation, ref index, eqLength, operand2);
                    else
                        operand2 = parseDivide(equation, ref index, eqLength, operand2);
                }

                if (operand2.Equals("Infinity")) return operand2;
                //if (operand2.Contains("E+") || operand2.Contains("E-"))
                //{
                //    operand2 = operand2.Contains("E+") ? replaceE_With0(operand2, "E+").result : replaceE_With0(operand2, "E-").result;
                //}
                bool isResultParsed = false;
                bool isOperand2Parsed = false;
                if (typeof(T) == typeof(decimal))
                {
                    decimal resultParsed = 0;
                    decimal operand2Parsed = 0;

                    isResultParsed = Decimal.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Decimal.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        decimal calcResult = resultParsed + operand2Parsed;

                        result = calcResult.ToString();
                    }
                    else return result = "Error";
                }
                else
                {
                    double resultParsed = 0;
                    double operand2Parsed = 0;

                    isResultParsed = Double.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Double.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        double calcResult = resultParsed + operand2Parsed;

                        result = calcResult.ToString();
                    }
                    else return result = "Error";
                }
            }
            if (!string.IsNullOrWhiteSpace(result))
            {
                result = parseSubtract(equation, ref index, eqLength, result);
            }


            if (!string.IsNullOrWhiteSpace(equation) && (string.IsNullOrWhiteSpace(result) || result.Equals("-")))
                index = equation.Length;

            //if does not contain math operator, increase index
            while (index < eqLength && (!equation[index].Equals('*') &&
                !equation[index].Equals('/') &&
                !equation[index].Equals('^') &&
                !equation[index].Equals('%') &&
                !equation[index].Equals('+') &&
                !equation[index].Equals('-')))
            {
                result += equation[index];
                ++index;
            }

            return result;
        }
        private string parseSubtract(string equation, ref int index, int eqLength, string result)
        {
            while (index < eqLength && equation[index].Equals('-'))
            {
                index++;
                string operand2 = getIsNumber(equation, ref index, eqLength);
                while (index < eqLength && (equation[index].Equals('*') || equation[index].Equals('/') || equation[index].Equals('^') || equation[index].Equals('%')))
                {
                    if (equation[index].Equals('^'))
                        operand2 = parseExponent(equation, ref index, eqLength, operand2);
                    else if (equation[index].Equals('%'))
                        operand2 = parseModulus(equation, ref index, eqLength, operand2);
                    else if (equation[index].Equals('*'))
                        operand2 = parseMultiply(equation, ref index, eqLength, operand2);
                    else
                        operand2 = parseDivide(equation, ref index, eqLength, operand2);
                }

                if (operand2.Equals("Infinity")) return operand2;
                //if (operand2.Contains("E+") || operand2.Contains("E-"))
                //{
                //    operand2 = operand2.Contains("E+") ? replaceE_With0(operand2, "E+").result : replaceE_With0(operand2, "E-").result;
                //}
                bool isResultParsed = false;
                bool isOperand2Parsed = false;
                if (typeof(T) == typeof(decimal))
                {
                    decimal resultParsed = 0;
                    decimal operand2Parsed = 0;

                    isResultParsed = Decimal.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Decimal.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        decimal calcResult = resultParsed - operand2Parsed;

                        result = calcResult.ToString();
                    }
                    else return result = "Error";
                }
                else
                {
                    double resultParsed = 0;
                    double operand2Parsed = 0;

                    isResultParsed = Double.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Double.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        double calcResult = resultParsed - operand2Parsed;

                        result = calcResult.ToString();
                    }
                    else return result = "Error";
                }
            }
            result = parseExponent(equation, ref index, eqLength, result);
            return result;
        }

        private string parseExponent(string equation, ref int index, int eqLength, string result)
        {
            while (index < eqLength && equation[index].Equals('^'))
            {
                index++;
                string operand2 = getIsNumber(equation, ref index, eqLength);

                if (typeof(T) == typeof(decimal) && (double.Parse(operand2) > 28d || double.Parse(operand2) < -28d))
                {
                    //throw new OverflowException();
                    return result = "Infinity";
                }


                //if (typeof(T) == typeof(decimal))
                //{
                //    decimal calcResult = (decimal)Math.Pow((double)Double.Parse(result), (double)Double.Parse(operand2));

                //    result = calcResult.ToString();
                //}
                //else
                //{
                bool isResultParsed = false;
                bool isOperand2Parsed = false;
                double resultParsed = 0;
                double operand2Parsed = 0;

                isResultParsed = Double.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                isOperand2Parsed = Double.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                if (isResultParsed && isOperand2Parsed)
                {
                    double calcResult = (double)Math.Pow(resultParsed, operand2Parsed);

                    result = calcResult.ToString();
                }
                else return result = "Error";
                //}
            }
            result = parseModulus(equation, ref index, eqLength, result);
            return result;
        }

        private string parseModulus(string equation, ref int index, int eqLength, string result)
        {
            while (index < eqLength && equation[index].Equals('%'))
            {
                index++;
                string operand2 = getIsNumber(equation, ref index, eqLength);

                while (index < eqLength && (equation[index].Equals('^')))
                {
                    if (equation[index].Equals('^'))
                    {
                        operand2 = parseExponent(equation, ref index, eqLength, operand2);
                        if (operand2.Equals("Infinity")) return operand2;
                        //if (operand2.Equals(double.PositiveInfinity.ToString())) return operand2;
                        //if (operand2.Equals(double.NegativeInfinity.ToString())) return operand2;

                    }
                }
                //if (operand2.Contains("E+") || operand2.Contains("E-"))
                //{
                //    operand2 = operand2.Contains("E+") ? replaceE_With0(operand2, "E+").result : replaceE_With0(operand2, "E-").result;
                //}
                bool isResultParsed = false;
                bool isOperand2Parsed = false;
                if (typeof(T) == typeof(decimal))
                {
                    decimal resultParsed = 0;
                    decimal operand2Parsed = 0;

                    isResultParsed = Decimal.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Decimal.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        if (operand2Parsed != 0)
                        {
                            decimal calcResult = resultParsed % operand2Parsed;

                            result = calcResult.ToString();
                        }
                        else return result = "Error";
                    }
                    else return result = "Error";
                }
                else
                {
                    double resultParsed = 0;
                    double operand2Parsed = 0;

                    isResultParsed = Double.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Double.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        if (operand2Parsed != 0)
                        {
                            double calcResult = resultParsed % operand2Parsed;

                            result = calcResult.ToString();
                        }
                        else return result = "Error";
                    }
                    else return result = "Error";
                }
            }
            result = parseMultiply(equation, ref index, eqLength, result);
            return result;
        }

        private string parseMultiply(string equation, ref int index, int eqLength, string result)
        {
            while (index < eqLength && equation[index].Equals('*'))
            {
                index++;
                string operand2 = getIsNumber(equation, ref index, eqLength);

                while (index < eqLength && (equation[index].Equals('^')))
                {
                    if (equation[index].Equals('^'))
                    {
                        operand2 = parseExponent(equation, ref index, eqLength, operand2);
                        if (operand2.Equals("Infinity")) return operand2;
                        //if (operand2.Equals(double.PositiveInfinity.ToString())) return operand2;
                        //if (operand2.Equals(double.NegativeInfinity.ToString())) return operand2;
                    }
                }
                //if (operand2.Contains("E+") || operand2.Contains("E-"))
                //{
                //    operand2 = operand2.Contains("E+") ? replaceE_With0(operand2, "E+").result : replaceE_With0(operand2, "E-").result;
                //}
                bool isResultParsed = false;
                bool isOperand2Parsed = false;
                if (typeof(T) == typeof(decimal))
                {
                    decimal resultParsed = 0;
                    decimal operand2Parsed = 0;

                    isResultParsed = Decimal.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Decimal.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        decimal calcResult = resultParsed * operand2Parsed;

                        result = calcResult.ToString();
                    }
                    else return result = "Error";
                }
                else
                {
                    double resultParsed = 0;
                    double operand2Parsed = 0;

                    isResultParsed = Double.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Double.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        double calcResult = resultParsed * operand2Parsed;

                        result = calcResult.ToString();
                    }
                    else return result = "Error";
                }
            }
            result = parseDivide(equation, ref index, eqLength, result);
            return result;
        }
        private string parseDivide(string equation, ref int index, int eqLength, string result)
        {
            while (index < eqLength && equation[index].Equals('/'))
            {
                index++;
                string operand2 = getIsNumber(equation, ref index, eqLength);

                while (index < eqLength && (equation[index].Equals('^')))
                {
                    if (equation[index].Equals('^'))
                    {
                        operand2 = parseExponent(equation, ref index, eqLength, operand2);
                        if (operand2.Equals("Infinity")) return operand2;
                        //if (operand2.Equals(double.PositiveInfinity.ToString())) return operand2;
                        //if (operand2.Equals(double.NegativeInfinity.ToString())) return operand2;

                    }
                }
                //if (operand2.Contains("E+") || operand2.Contains("E-"))
                //{
                //    operand2 = operand2.Contains("E+") ? replaceE_With0(operand2, "E+").result : replaceE_With0(operand2, "E-").result;
                //}

                bool isResultParsed = false;
                bool isOperand2Parsed = false;
                if (typeof(T) == typeof(decimal))
                {
                    decimal resultParsed = 0;
                    decimal operand2Parsed = 0;

                    isResultParsed = Decimal.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Decimal.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        if (operand2Parsed != 0)
                        {
                            //decimal calcResult = (decimal)Decimal.Parse(result, System.Globalization.NumberStyles.Any) / (decimal)Decimal.Parse(operand2, System.Globalization.NumberStyles.Any);
                            decimal calcResult = resultParsed / operand2Parsed;

                            result = calcResult.ToString();
                        }
                        else return result = "Error:  Divided By Zero ";
                    }
                    else return result = "Error";
                }
                else
                {
                    double resultParsed = 0;
                    double operand2Parsed = 0;

                    isResultParsed = Double.TryParse(result, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out resultParsed);
                    isOperand2Parsed = Double.TryParse(operand2, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out operand2Parsed);
                    if (isResultParsed && isOperand2Parsed)
                    {
                        if (operand2Parsed != 0)
                        {
                            //double calcResult = (double)Double.Parse(result, System.Globalization.NumberStyles.Any) / (double)Double.Parse(operand2, System.Globalization.NumberStyles.Any);
                            double calcResult = resultParsed / operand2Parsed;

                            result = calcResult.ToString();
                        }
                        else return result = "Error:  Divided By Zero ";
                    }
                    else return result = "Error";
                }
            }


            return result;
        }
        private string getIsNumber(string equation, ref int index, int eqLength)
        {
            string returnVal = "";
            bool isNegVal = true;
            bool isE = false;
            while ((index < eqLength) &&
            (
                  (isNegVal && (int)equation[index] == (int)'-') ||
                  (!isNegVal && !isE && (int)equation[index] == (int)'E') ||
                  ((int)equation[index] == (int)double.PositiveInfinity.ToString()[0]) ||
                  ((int)equation[index] == (int)'.') ||
                ((int)equation[index] >= (int)'0' && (int)equation[index] <= (int)'9'))
                  )
            {
                returnVal += equation[index];
                if (isNegVal)
                    isNegVal = false;
                if ((int)equation[index] == (int)'E')
                {
                    isE = true;
                    isNegVal = true;

                    index++;
                    returnVal += equation[index];
                }
                index++;
            }
            if (index < eqLength && returnVal.Equals("", StringComparison.CurrentCulture))
            {
                index++;
            }

            return returnVal;
        }





        //"-7.34641020664359E-06+1"
        public string replaceE(string data, string dataToReplace)
        {


            int indexOfTime = 0;
            //try
            //{
            int numberAfterE_Index = 0;
            if (data != null)
            {
                while (data.IndexOf(dataToReplace, indexOfTime, StringComparison.CurrentCulture) > -1)
                {
                    indexOfTime = data.IndexOf(dataToReplace, StringComparison.CurrentCulture);
                    if (indexOfTime - 1 >= 0 && indexOfTime + dataToReplace.Length <= data.Length - 1)
                    {
                        if (
                            isNumber(data[indexOfTime - 1].ToString()) &&
                            isNumber(data[indexOfTime + dataToReplace.Length].ToString())
                            )
                        {
                            numberAfterE_Index = indexOfTime + 2;
                            //numberAfterE_Index = 0;
                            //while (isNumber(data[indexOfTime].ToString()))
                            //{
                            //    numberAfterE_Index++;
                            //}
                            if (dataToReplace.Equals("E-"))
                                data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, "*10^-");
                            else if (dataToReplace.Equals("E+"))
                                data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, "*10^");
                        }
                    }
                    indexOfTime++;
                }
            }
            //}
            //catch { }

            return data;
        }
        public (string result, int number0Added) replaceE_With0(string data, string dataToReplace)
        {

            int number0Added = 0;
            int indexOfTime = 0;
            //try
            //{
            int numberAfterE_Index = 0;
            if (data != null)
            {
                while (data.Length - 1 >= indexOfTime && data.IndexOf(dataToReplace, indexOfTime, StringComparison.CurrentCulture) > -1)
                {
                    indexOfTime = data.IndexOf(dataToReplace, StringComparison.CurrentCulture);
                    if (indexOfTime - 1 >= 0 && indexOfTime + dataToReplace.Length <= data.Length - 1)
                    {
                        if (
                            isNumber(data[indexOfTime - 1].ToString()) &&
                            isNumber(data[indexOfTime + dataToReplace.Length].ToString())
                            )
                        {
                            numberAfterE_Index = indexOfTime + 2;

                            if (dataToReplace.Equals("E-") || dataToReplace.Equals("E+"))
                            {
                                string subStr = data.Substring(numberAfterE_Index);
                                if (subStr.Contains('+') ||
                                    subStr.Contains('-') ||
                                    subStr.Contains('*') ||
                                    subStr.Contains('/') ||
                                    subStr.Contains('^') ||
                                    subStr.Contains('%')
                                    )
                                {
                                    data = replaceE(data, "E-");
                                    data = replaceE(data, "E+");
                                    break;
                                }
                            }

                            //numberAfterE_Index = 0;
                            //while (isNumber(data[indexOfTime].ToString()))
                            //{
                            //    numberAfterE_Index++;
                            //}
                            if (dataToReplace.Equals("E-"))
                            {
                                int numOfZero = int.Parse(data.Substring(numberAfterE_Index));
                                data = data.Remove(indexOfTime);
                                int insert0At = data.StartsWith('-') ? 1 : 0;
                                //numOfZero -= data.Length - 1;//UNCOMMENT IF ERROR
                                numOfZero -= (data.IndexOf('.') > -1) ? data.IndexOf('.') - 1 : 0;

                                while (numOfZero > 0)
                                {
                                    data = data.Insert(insert0At, "0");
                                    numOfZero--;
                                    number0Added++;
                                }
                                data = data.Replace(".", "").Insert(insert0At + 1, ".");

                                //data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, "*10^" + data.Substring(numberAfterE_Index, numberAfterE_Index));
                            }
                            else if (dataToReplace.Equals("E+"))
                            {
                                int numOfZero = int.Parse(data.Substring(numberAfterE_Index));
                                data = data.Remove(indexOfTime);
                                numOfZero -= (data.IndexOf('.') > -1) ? data.IndexOf('.') : 0;
                                int indexOfDecimal = data.IndexOf(".");
                                int lengthAfterDecimal = 0;
                                if (data.Contains("."))
                                {
                                    lengthAfterDecimal = data.Substring(data.IndexOf(".") + 1).Length;
                                    data = data.Remove(data.IndexOf("."), 1);
                                    numOfZero -= lengthAfterDecimal - 1;
                                }

                                while (numOfZero > 0)
                                {
                                    data += "0";
                                    numOfZero--;
                                    number0Added++;
                                }
                                if (indexOfDecimal != -1 && indexOfDecimal + number0Added + lengthAfterDecimal < data.Length)
                                {
                                    data = data.Insert(indexOfDecimal + number0Added + lengthAfterDecimal, ".");
                                }

                                //data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, "*10^" + data.Substring(numberAfterE_Index, numberAfterE_Index));
                            }
                        }
                    }
                    indexOfTime++;
                }
            }
            //}
            //catch
            //{
            //    //data = replaceE(data, "E-");
            //    //data = replaceE(data, "E+");
            //}

            return (data, number0Added);
        }



        //=================================================================================SolvingFormula
        //=================================================================================SolvingFormula
        //=================================================================================SolvingFormula
        //(bool, List<string>) containsSolvableFunction(string formula) {
        //    (bool, List<string>) solvableFunctions = (false,null);
        //    if (formula.Contains("vec2(", StringComparison.CurrentCulture)) {
        //        solvableFunctions = (true, solvableFunctions.Item2);
        //        solvableFunctions.Item2.Add("vec2(");
        //    }
        //    return solvableFunctions;
        //}
        private string solveBasicEquation(string formula, string functionName = "", bool convertETo0 = true)
        {
            //////////if (formula.Contains("E+") || formula.Contains("E-"))
            //////////{
            //////////    formula = formula.Contains("E+") ? replaceE_With0(formula, "E+").result : replaceE_With0(formula, "E-").result;
            //////////}
            //formula = replaceE(formula, "E-");
            //formula = replaceE(formula, "E+");
            if (
                (formula.Contains("(") || formula.Contains(")")) &&
                formula.IndexOf(',') < 0
                //!containsSolvableFunction(formula).Item1
                )
                if (!string.IsNullOrWhiteSpace(functionName))
                    return functionName + "(" + formula + ")";
                else
                    return formula;
            int chkIndex = 0;
            int eqLength = formula.Length;
            (string result, int numberOf0Added) result = ("0", 0);
            if (formula.IndexOf(',') >= 0/* && !containsSolvableFunction(formula)*/ && !string.IsNullOrWhiteSpace(functionName)) return functionName + "(" + formula + ")";
            else if (formula.IndexOf(',') >= 0 /*&& !containsSolvableFunction(formula).Item1*/) return formula;
            //if (containsSolvableFunction(formula).Item1)
            //{
            //    int lastIndex = new StringVue().lastIndexOf(formula, ",", formula.IndexOf("vec2("))
            //    string x = formula.Substring(formula.IndexOf("vec2(") + "vec2(".Length, lastIndex);
            //}
            //else
            //{
            while (chkIndex < eqLength)
            {
                //////////if (result.result.Contains("E+") || result.result.Contains("E-"))
                //////////{
                //////////    result = result.result.Contains("E+") ? replaceE_With0(result.result, "E+") : replaceE_With0(result.result, "E-");
                //////////    //chkIndex += result.numberOf0Added;
                //////////    //eqLength += result.numberOf0Added;
                //////////}
                //result = replaceE(result, "E-");
                //result = replaceE(result, "E+");
                //cout<<"2: "<<newFormula<<endl;
                result.result = parseAdd(formula, ref chkIndex, eqLength, result.result);
                if (isErrorSkip(result.result))
                {
                    return result.result;
                }
                //if (result.result.Equals(double.PositiveInfinity.ToString()))
                //{
                //    throw new Exception("Positive Infinity Error");
                //}
                //else if (result.result.Equals(double.NegativeInfinity.ToString()))
                //{
                //    throw new Exception("Negative Infinity Error");
                //}
                //break;
            }
            //}

            if (!string.IsNullOrWhiteSpace(functionName) && string.IsNullOrWhiteSpace(result.result) && !string.IsNullOrWhiteSpace(formula) && !isNumber(formula))
            {
                result.result = functionName + "(" + formula + ")";
            }
            else if (string.IsNullOrWhiteSpace(result.result) && !string.IsNullOrWhiteSpace(formula) && !isNumber(formula))
            {
                result.result = formula;
            }
            else if (string.IsNullOrWhiteSpace(result.result) && !string.IsNullOrWhiteSpace(formula))
            {
                result.result = "Error";
            }
            //if (!string.IsNullOrWhiteSpace(functionName) && functionName.Equals("sum"))
            //{
            //    result.result = functionName+"("+ result.result+")";
            //}
            //////////if (result.result.Contains("E+") || result.result.Contains("E-"))
            //////////{
            //////////    result = result.result.Contains("E+") ? replaceE_With0(result.result, "E+") : replaceE_With0(result.result, "E-");
            //////////    //chkIndex += result.numberOf0Added;
            //////////    //eqLength += result.numberOf0Added;
            //////////}

            return result.result;
        }

        Dictionary<string, bool> isFunctionTo_SolveFirst_isAddPar = new Dictionary<string, bool>() { { "ifThen", false }, { "orThen", false } };
        private string solveFunctions_Custom(string formula, string functionName, Formula formula_Obj, bool solveTrigFunction = false, bool assignRearrange_OnMainThread = true)
        {
            /* FOR TESTING
            FunctionCollection functionVue = FunctionCollection();
            functionVue.setFunctionList_Custom_TEST();
            string result = formula;
            result = functionVue.customFunctionList[functionName].rearrangedFormula;
            */
            string result = "";
            string use = "";
            //foreach (var v in FunctionCollection_MVVM.functionCollection.customFunctionCollection)
            //{

            if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(functionName))
            {
                result = FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[functionName].formulaObj.rearrangedFormula;
                use = FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[functionName].use;
                use = use.Remove(use.LastIndexOf(")")).Remove(0, use.IndexOf("(") + 1);
            }
            //}


            if (string.IsNullOrWhiteSpace(result) ||
                result.Equals("", StringComparison.CurrentCulture))
                result = formula;
            else
            {
                //result = solveFunctions_Default(formula, functionName);

                //if (!string.IsNullOrWhiteSpace(result) ||
                //    !result.Equals("", StringComparison.CurrentCulture))
                //{
                int formulaLength = formula.Length;
                if (new StringVue().indexOf(use, ",") != -1)
                {
                    Dictionary<string, string> splitStr_Use = null;
                    if (!string.IsNullOrWhiteSpace(use))
                        splitStr_Use = new StringVue().splitString_BaseOnComma(use);
                    Dictionary<string, string> splitStr = new StringVue().splitString_BaseOnComma(formula, splitStr_Use,
                        splitStr_Ignore_SolveFunctions_DefaultMultiVariable(formula));

                    if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[functionName].ignoreFunctionForGraph)
                    {
                        bool isSameKeyValue = true;
                        if (formula_Obj != null)
                        {
                            if (splitStr != null)
                            {
                                foreach (var v in formula_Obj.variableList_Bind)
                                {
                                    foreach (var k in splitStr)
                                    {
                                        if (v.name == k.Key)
                                        {
                                            if (v.value != k.Value)
                                            {
                                                isSameKeyValue = false;
                                            }
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        string returningFormula = "";

                        if (formula_Obj != null)
                        {
                            if (splitStr != null)
                            {
                                int length = splitStr.Count;
                                for (int i = 0; i < length; i++)
                                {
                                    //splitStr[splitStr.ElementAt(i).Key] = solveParenthesis_Functions(splitStr[splitStr.ElementAt(i).Key], formula_Obj, true, false, assignRearrange_OnMainThread);

                                    if (!isSameKeyValue)
                                    {
                                        returningFormula += splitStr[splitStr.ElementAt(i).Key] + ",";
                                    }
                                }
                            }
                        }
                        else
                        {
                            returningFormula = formula;
                        }
                        if (!isSameKeyValue)
                        {
                            return functionName + "(" + returningFormula.Remove(returningFormula.LastIndexOf(',')) + ")";
                        }
                    }

                    //Dictionary<string, string> splitStr_UseMain = new Dictionary<string, string>();
                    //if (splitStr_Use != null)
                    //{
                    //    for (int i = 0;i< splitStr_Use.Count;i++) {
                    //        splitStr_UseMain.Add(splitStr_Use.ElementAt(i).Key, splitStr.ElementAt(i).Value);
                    //    }
                    //}
                    if (solveTrigFunction && is_SolveFunctions_DefaultMultiVariable(functionName).Item1)
                        result = solveFunctions_DefaultMultiVariable(formula_Obj, formula, functionName, splitStr, assignRearrange_OnMainThread);
                    else
                        result = setVariable(formula_Obj, result, splitStr, solveTrigFunction);

                    if (isFunctionTo_SolveFirst_isAddPar.ContainsKey(functionName))
                    {
                        result = solveFunctions_DefaultMultiVariable(formula_Obj, formula, functionName, splitStr, assignRearrange_OnMainThread);
                    }
                    if (solveTrigFunction)
                    {
                        result = solveParenthesis_Functions(result, formula_Obj, true, true, assignRearrange_OnMainThread);
                    }

                }
                else
                {
                    Dictionary<string, string> splitStr = new Dictionary<string, string>() { { formula, formula } };
                    //if (solveTrigFunction)
                    //{
                    //    result = result.Replace("x", formula);
                    //}
                    result = setVariable(formula_Obj, result, splitStr, false, true);
                    if (solveTrigFunction)
                    {
                        formula = result.Remove(new StringVue().indexOf(result, ")", result.IndexOf("("), true)).Remove(0, result.IndexOf("(") + 1);
                        //if (isNumber(formula.Replace("(", "").Replace(")", "")) || formula.Contains(','))
                        result = solveFunctions_Default(formula_Obj, formula, functionName, assignRearrange_OnMainThread);
                    }
                }
                //}
                if (!isFunctionTo_SolveFirst_isAddPar.ContainsKey(functionName) && result.Length>0 && result[0].Equals('(') && result[result.Length - 1].Equals(')'))
                {
                    result = result.Remove(result.Length - 1).Remove(0,1);
                }
            }
            //Debug.WriteLine("test1XXXXXXX: " + result);
            return result;
        }




        private (bool, List<string> variablesToIgnore, List<int> columnsToIgnore, int totalNumberOfColumns, bool ignoreAllVar) is_SolveFunctions_DefaultMultiVariable(string functionName)
        {
            //column of -1 = no column to ignore
            (bool, List<string>, List<int>, int, bool) isFunc_skipVar_atColumn = (false, null, null, -1, false);
            if (functionName.Equals("sum", StringComparison.CurrentCulture))
                return (true, new List<string>() { "i" }, new List<int>() { 2 }, totalNumberOfColumns: 3, ignoreAllVar: false);
            if (functionName.Equals("refUpdate", StringComparison.CurrentCulture))
                return (true, new List<string>() { "i", "ref" }, new List<int>() { 3 }, totalNumberOfColumns: 4, ignoreAllVar: false);
            if (functionName.Equals("atan2", StringComparison.CurrentCulture))
                return (true, new List<string>() { }, new List<int>() { -1 }, totalNumberOfColumns: 2, ignoreAllVar: false);
            if (functionName.Equals("formula", StringComparison.CurrentCulture))
                return (true, new List<string>() { }, new List<int>() { 1 }, totalNumberOfColumns: 2, ignoreAllVar: true);
            if (functionName.Equals("ifThen", StringComparison.CurrentCulture))
                return (true, new List<string>() { ">", "<", ">=", "<=", "!=", "==", "true", "false", "throw" }, new List<int>() { 0, 1, 2, 3, 4 }, totalNumberOfColumns: 5, ignoreAllVar: false);
            if (functionName.Equals("orThen", StringComparison.CurrentCulture))
                return (true, new List<string>() { "true", "false", "throw" }, new List<int>() { 0, 1, 2, 3 }, totalNumberOfColumns: 4, ignoreAllVar: false);
            if (functionName.Equals("clamp", StringComparison.CurrentCulture))
                return (true, new List<string>() { }, new List<int>() { -1 }, totalNumberOfColumns: 3, ignoreAllVar: false);
            if (functionName.Equals("random", StringComparison.CurrentCulture))
                return (true, new List<string>() { }, new List<int>() { -1 }, totalNumberOfColumns: 2, ignoreAllVar: false);
            return isFunc_skipVar_atColumn;
        }
        Dictionary<string, List<int>> splitStr_Ignore_SolveFunctions_DefaultMultiVariable(string formula)
        {
            Dictionary<string, List<int>> ignoreFor_SplitString = new Dictionary<string, List<int>>();
            List<string> customFunctionName_Multivariable_List = new List<string>() { 
                //default functions
                "sum",
                "refUpdate",
                "atan2",
                "ifThen",
                "orThen",
                "clamp",
                "random",

                //graph functions
                "vec2",
                "instance",

                //formula, //not needed since will be replaced before calc

            };

            foreach (string s in customFunctionName_Multivariable_List)
            {
                int index = 0;
                int index1 = 0;
                while (new StringVue().indexOf(formula, s + "(", index) >= 0)
                {
                    index = new StringVue().indexOf(formula, s + "(", index);

                    index1 = index + s.Length;
                    index1 = new StringVue().indexOf(formula, ")", index1, true);
                    if (!ignoreFor_SplitString.ContainsKey(s))
                        ignoreFor_SplitString.Add(s, new List<int>() { index, index1 });
                    else
                    {

                        ignoreFor_SplitString[s].Add(index);
                        ignoreFor_SplitString[s].Add(index1);

                    }
                    index = index1;
                }
            }
            return ignoreFor_SplitString;
        }

        private bool isEqualToVariable_InList(string value, List<string> varList)
        {
            foreach (string variable in varList)
            {
                if (variable.Equals(value, StringComparison.CurrentCulture))
                {
                    return true;
                }
            }
            return false;
        }
        private bool isEqualToVariableColumn_InList(int value, List<int> colList)
        {
            foreach (int col in colList)
            {
                if (col == value)
                {
                    return true;
                }
            }
            return false;
        }

        //used for refUpdate and sum
        private string solveFunctions_DefaultMultiVariable(Formula formula_Obj, string functionExpression, string functionName, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread)
        {
            //numOfNestedFunc
            double parsedValue = 0;
            string result = "";
            if (functionName.Equals("sum", StringComparison.CurrentCulture))
            {
                (string result, bool isNested_I, bool isRefUpdate_Nested) sumState = summation(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                if (sumState.isNested_I)
                {
                    (string result, bool isNested_I, bool isRefUpdate_Nested) refUpResult = summation(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                    result = refUpResult.Item1;
                    sumState.isRefUpdate_Nested = refUpResult.isRefUpdate_Nested;
                }
                else
                    result = summation(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread).Item1;
                if (sumState.isNested_I && sumState.isRefUpdate_Nested)
                {
                    result = "sum(" + splitStr["i"] + "," + splitStr["n"] + "," + summation(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread, false, true).Item1 + ")";
                }
                else if (sumState.isNested_I)
                {
                    result = "sum(" + splitStr["i"] + "," + splitStr["n"] + "," + result + ")";
                }

            }
            else if (functionName.Equals("refUpdate", StringComparison.CurrentCulture))
            {
                (string result, bool isNested_I, bool isRefUpdate_Nested) refUpdateState = refUpdate(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread, true);
                if (refUpdateState.isNested_I)
                {
                    (string result, bool isNested_I, bool isRefUpdate_Nested) refUpResult = refUpdate(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                    result = refUpResult.Item1;
                    refUpdateState.isRefUpdate_Nested = refUpResult.isRefUpdate_Nested;
                }
                else
                    result = refUpdate(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread).Item1;
                if (refUpdateState.isNested_I && refUpdateState.isRefUpdate_Nested)
                {
                    result = "refUpdate(" + splitStr["i"] + "," + splitStr["n"] + "," + splitStr["ref"] + "," + refUpdate(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread, false, true).Item1 + ")";
                }
                else if (refUpdateState.isNested_I)
                {
                    result = "refUpdate(" + splitStr["i"] + "," + splitStr["n"] + "," + splitStr["ref"] + "," + result + ")";
                }
            }
            else if (functionName.Equals("atan2", StringComparison.CurrentCulture))
            {
                bool parsed1 = double.TryParse(solveParenthesis_Functions(splitStr["x"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                bool parsed2 = double.TryParse(solveParenthesis_Functions(splitStr["y"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);


                if (parsed1 && parsed2)
                    result = atan2(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                else
                    result = "atan2(" + functionExpression + ")";
            }
            else if (functionName.Equals("ifThen", StringComparison.CurrentCulture))
            {
                bool parsed1 = double.TryParse(solveParenthesis_Functions(splitStr["x"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                bool parsed2 = double.TryParse(solveParenthesis_Functions(splitStr["y"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);

                if ((parsed1 && parsed2)||
                    
                    ((splitStr["x"].Equals("true") ||
                    splitStr["x"].Equals("false"))
                    
                    &&

                    (splitStr["y"].Equals("true") ||
                    splitStr["y"].Equals("false")))
                    )
                    result = ifThen(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                else
                    result = "ifThen(" + functionExpression + ")";
            }
            else if (functionName.Equals("orThen", StringComparison.CurrentCulture))
            {
                bool parsed1 = double.TryParse(solveParenthesis_Functions(splitStr["x"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                bool parsed2 = double.TryParse(solveParenthesis_Functions(splitStr["y"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);

                if ((parsed1 && parsed2) ||
                    ((splitStr["x"].Equals("true") ||
                    splitStr["x"].Equals("false"))

                    &&

                    (splitStr["y"].Equals("true") ||
                    splitStr["y"].Equals("false")))
                    )
                    result = orThen(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                else
                    result = "orThen(" + functionExpression + ")";

            }
            else if (functionName.Equals("clamp", StringComparison.CurrentCulture))
            {
                bool parsed1 = double.TryParse(solveParenthesis_Functions(splitStr["min"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                bool parsed2 = double.TryParse(solveParenthesis_Functions(splitStr["max"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                bool parsed3 = double.TryParse(solveParenthesis_Functions(splitStr["value"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                if (parsed1 && parsed2 && parsed3)
                    result = clamp(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                else
                    result = "clamp(" + functionExpression + ")";
            }
            else if (functionName.Equals("random", StringComparison.CurrentCulture))
            {
                bool parsed1 = double.TryParse(solveParenthesis_Functions(splitStr["min"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                bool parsed2 = double.TryParse(solveParenthesis_Functions(splitStr["max"], null, true, true, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out parsedValue);
                if (parsed1 && parsed2)
                    result = random(formula_Obj, functionExpression, splitStr, assignRearrange_OnMainThread);
                else
                    result = "random(" + functionExpression + ")";
            }

            return result;

        }

        private string random(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread)
        {

            double min = 0;
            double max = 0;

            bool minParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["min"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out min);
            bool maxParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["max"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out max);
            if (minParsed &&
            maxParsed)
            {
                if (min <= max)
                    return new Random().Next((int)min, (int)max).ToString();
                else return "Error";
            }
            else return "Error";
        }

        private string clamp(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread)
        {
            if (typeof(T).Equals(typeof(decimal)))
            {

                decimal value = 0;


                decimal min = 0;
                decimal max = 0;

                bool valueParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["value"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out value);


                bool minParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["min"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out min);
                bool maxParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["max"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out max);

                if (valueParsed && minParsed && maxParsed)
                {
                    if (min <= max)
                        return Math.Clamp(value, min, max).ToString();
                    else return "Error";

                }
                else return "Error";

            }
            else
            {

                double value = 0;


                double min = 0;
                double max = 0;

                bool valueParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["value"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out value);


                bool minParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["min"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out min);
                bool maxParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["max"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out max);

                if (valueParsed && minParsed && maxParsed)
                {
                    if (min <= max)
                        return Math.Clamp(value, min, max).ToString();
                    else return "Error";

                }
                else return "Error";
            }
        }

        private string atan2(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread)
        {
            if (typeof(T).Equals(typeof(decimal)))
            {

                decimal x = 0;


                decimal y = 0;


                bool xParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x);


                bool yParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["y"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out y);

                if (xParsed && yParsed)
                {
                    return Math.Atan2((double)x, (double)y).ToString();
                }
                else return "Error";

            }
            else
            {

                double x = 0;


                double y = 0;


                bool xParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x);


                bool yParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["y"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out y);

                if (xParsed && yParsed)
                {
                    return Math.Atan2(x, y).ToString();
                }
                else return "Error";
            }
        }

        private (string result, bool isNested_I, bool isRefUpdate_Nested) refUpdate(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread, bool checkIsIParsedFalse = false, bool isNested = false)
        {
            if (typeof(T).Equals(typeof(decimal)))
            {
                decimal i = 0;

                decimal n = 0;

                decimal ref1 = 0;

                bool iParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["i"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out i);

                bool nParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["n"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out n);

                bool ref1Parsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["ref"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out ref1);


                decimal x = ref1;
                decimal xResult = 0;
                decimal addResult = 0;
                if (checkIsIParsedFalse && iParsed) return (splitStr["x"], false, false);
                if (!iParsed)
                {
                    if (checkIsIParsedFalse)
                    {
                        return (splitStr["x"], true, false);
                    }
                    else if (!isNested)
                    {
                        return (splitStr["x"], true, true);
                    }
                    else
                    {
                        return (splitStr["x"], true, true);
                        //string defaultI = "";
                        //string defaultN = "";
                        //string defaultRef = "";
                        //if (!iParsed || !nParsed || !ref1Parsed)
                        //{
                        //    if (iParsed)
                        //    {
                        //        defaultI = splitStr["i"];
                        //        splitStr["i"] = "i";
                        //    }

                        //    if (nParsed)
                        //    {
                        //        defaultN = splitStr["n"];
                        //        splitStr["n"] = "n";
                        //    }

                        //    if (ref1Parsed)
                        //    {
                        //        defaultRef = splitStr["ref"];
                        //        splitStr["ref"] = "ref";
                        //    }
                        //}

                        //var setNestedRefUpdate = setVariable(formula_Obj, splitStr["x"], splitStr, false, false, null, false);

                        //splitStr["i"] = defaultI;
                        //splitStr["n"] = defaultN;
                        //splitStr["ref"] = defaultRef;

                        //return (setNestedRefUpdate, true, true);
                    }
                }
                else if (iParsed && nParsed && ref1Parsed &&
                    new StringVue().replaceData(setVariable(formula_Obj, splitStr["x"], splitStr, false, false, new List<string>() { "ref" }), "ref", x.ToString()).containsDataToReplace)
                {
                    i = Math.Floor(i);
                    n = Math.Floor(n);
                    string splitStr_Default = splitStr["i"];

                    Dictionary<string, string> splitStr_RemoveX = new Dictionary<string, string>(splitStr);
                    splitStr_RemoveX["x"] = "x";
                    for (; i <= n; i++)
                    {
                        splitStr["i"] = i.ToString();
                        splitStr_RemoveX["i"] = i.ToString();

                        if (!decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr_RemoveX), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out xResult))
                        {
                            return ("Error", false, false);
                        }
                        else
                        {
                            addResult = xResult;
                            splitStr_RemoveX["ref"] = xResult.ToString();
                            splitStr["ref"] = xResult.ToString();
                        }


                        if (!decimal.TryParse(solveParenthesis_Functions(
                            new StringVue().replaceData(setVariable(formula_Obj, addResult.ToString(), splitStr_RemoveX, false, false, new List<string>() { "ref" }), "ref", x.ToString()).getNewData,
                            formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x))
                        {
                            return ("Error", false, false);
                        }

                    }
                    splitStr["i"] = splitStr_Default;
                    splitStr_RemoveX["i"] = splitStr_Default;
                    return (x.ToString(), false, false);
                }
                else if (new StringVue().replaceData(splitStr["x"], "ref", "").containsDataToReplace)
                {
                    if (iParsed)
                        return (new StringVue().replaceData(splitStr["x"], "i", i.ToString()).getNewData, false, false);
                    else
                        return (new StringVue().replaceData(splitStr["x"], "i", splitStr["i"], true).getNewData, false, false);
                }

                else return ("Error", false, false);

            }
            else
            {

                double i = 0;

                double n = 0;

                double ref1 = 0;

                bool iParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["i"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out i);

                bool nParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["n"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out n);

                bool ref1Parsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["ref"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out ref1);


                double x = ref1;
                double xResult = 0;
                double addResult = 0;
                if (checkIsIParsedFalse && iParsed) return (splitStr["x"], false, true);
                if (!iParsed)
                {
                    if (checkIsIParsedFalse)
                    {
                        return (splitStr["x"], true, true);
                    }
                    else if (!isNested)
                    {
                        return (splitStr["x"], true, true);
                    }
                    else
                    {
                        return (splitStr["x"], true, true);
                        //string defaultI = "";
                        //string defaultN = "";
                        //string defaultRef = "";
                        //if (!iParsed || !nParsed || !ref1Parsed)
                        //{
                        //    if (iParsed)
                        //    {
                        //        defaultI = splitStr["i"];
                        //        splitStr["i"] = "i";
                        //    }

                        //    if (nParsed)
                        //    {
                        //        defaultN = splitStr["n"];
                        //        splitStr["n"] = "n";
                        //    }

                        //    if (ref1Parsed)
                        //    {
                        //        defaultRef = splitStr["ref"];
                        //        splitStr["ref"] = "ref";
                        //    }
                        //}

                        //var setNestedRefUpdate = setVariable(formula_Obj, splitStr["x"], splitStr, false, false, null, false);

                        //splitStr["i"] = defaultI;
                        //splitStr["n"] = defaultN;
                        //splitStr["ref"] = defaultRef;

                        //return (setNestedRefUpdate, true, true);
                    }
                }
                else if (iParsed && nParsed && ref1Parsed
                    )
                {
                    i = Math.Floor(i);
                    n = Math.Floor(n);
                    string splitStr_Default = splitStr["i"];

                    Dictionary<string, string> splitStr_RemoveX = new Dictionary<string, string>(splitStr);
                    splitStr_RemoveX["x"] = "x";
                    for (; i <= n; i++)
                    {
                        splitStr["i"] = i.ToString();

                        splitStr_RemoveX["i"] = i.ToString();


                        if (!double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr_RemoveX), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out xResult))
                        {
                            return ("Error", false, false);
                        }
                        else
                        {
                            addResult = xResult;
                            splitStr_RemoveX["ref"] = xResult.ToString();
                            splitStr["ref"] = xResult.ToString();
                        }


                        if (!double.TryParse(solveParenthesis_Functions(
                            new StringVue().replaceData(setVariable(formula_Obj, addResult.ToString(), splitStr_RemoveX, false, false, new List<string>() { "ref" }), "ref", x.ToString()).getNewData,
                            formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x))
                        {
                            return ("Error", false, false);
                        }

                    }
                    splitStr_RemoveX["i"] = splitStr_Default;
                    splitStr["i"] = splitStr_Default;
                    return (x.ToString(), false, false);
                }
                else if (new StringVue().replaceData(splitStr["x"], "ref", "").containsDataToReplace)
                {
                    if (iParsed)
                        return (new StringVue().replaceData(splitStr["x"], "i", i.ToString()).getNewData, false, false);
                    else
                        return (new StringVue().replaceData(splitStr["x"], "i", splitStr["i"], true).getNewData, false, false);
                }

                else return ("Error", false, false);

            }



        }
        private (string result, bool isNested_I, bool isRefUpdate_Nested) summation(Formula formula_Obj, string functionExpression, Dictionary<string, string> splitStr, bool assignRearrange_OnMainThread, bool checkIsIParsedFalse = false, bool isNested = false)
        {

            if (typeof(T).Equals(typeof(decimal)))
            {
                decimal i = 0;

                decimal n = 0;

                bool iParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["i"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out i);

                bool nParsed = decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["n"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out n);

                decimal x = 0;

                if (checkIsIParsedFalse && iParsed) return (splitStr["x"], false, false);
                if (!iParsed)
                {
                    if (checkIsIParsedFalse)
                    {
                        return (splitStr["x"], true, false);
                    }
                    else if (!isNested)
                    {
                        return (splitStr["x"], true, true);
                    }
                    else
                    {
                        return (splitStr["x"], true, true);
                        //string defaultI = "";
                        //string defaultN = "";
                        //if (!iParsed || !nParsed)
                        //{
                        //    if (iParsed)
                        //    {
                        //        defaultI = splitStr["i"];
                        //        splitStr["i"] = "i";
                        //    }

                        //    if (nParsed)
                        //    {
                        //        defaultN = splitStr["n"];
                        //        splitStr["n"] = "n";
                        //    }

                        //}

                        //var setNestedRefUpdate = setVariable(formula_Obj, splitStr["x"], splitStr, false, false, null, false);

                        //splitStr["i"] = defaultI;
                        //splitStr["n"] = defaultN;

                        //return (setNestedRefUpdate, true, true);
                    }
                }
                else if (iParsed && nParsed)
                {
                    i = Math.Floor(i);
                    n = Math.Floor(n);
                    string splitStr_Default = splitStr["i"];

                    Dictionary<string, string> splitStr_RemoveX = new Dictionary<string, string>(splitStr);
                    splitStr_RemoveX["x"] = "x";
                    for (; i <= n; i++)
                    {
                        splitStr["i"] = i.ToString();
                        splitStr_RemoveX["i"] = i.ToString();
                        decimal result;
                        if (decimal.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr_RemoveX), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                            x += result;
                        else return ("Error", false, false);

                    }
                    splitStr_RemoveX["i"] = splitStr_Default;
                    splitStr["i"] = splitStr_Default;
                    return (x.ToString(), false, false);
                }

                else if (iParsed)
                    return (new StringVue().replaceData(splitStr["x"], "i", i.ToString()).getNewData, false, false);
                else return ("Error", false, false);

            }
            else
            {

                double i = 0;

                double n = 0;

                bool iParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["i"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out i);

                bool nParsed = double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["n"], splitStr), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out n);

                double x = 0;

                if (checkIsIParsedFalse && iParsed) return (splitStr["x"], false, false);
                if (!iParsed)
                {
                    if (checkIsIParsedFalse)
                    {
                        return (splitStr["x"], true, false);
                    }
                    else if (!isNested)
                    {
                        return (splitStr["x"], true, true);
                    }
                    else
                    {
                        return (splitStr["x"], true, true);
                        //string defaultI = "";
                        //string defaultN = "";
                        //if (!iParsed || !nParsed)
                        //{
                        //    if (iParsed)
                        //    {
                        //        defaultI = splitStr["i"];
                        //        splitStr["i"] = "i";
                        //    }

                        //    if (nParsed)
                        //    {
                        //        defaultN = splitStr["n"];
                        //        splitStr["n"] = "n";
                        //    }

                        //}

                        //var setNestedRefUpdate = setVariable(formula_Obj, splitStr["x"], splitStr, false, false, null, false);

                        //splitStr["i"] = defaultI;
                        //splitStr["n"] = defaultN;

                        //return (setNestedRefUpdate, true, true);
                    }
                }
                else if (iParsed && nParsed)
                {
                    i = Math.Floor(i);
                    n = Math.Floor(n);
                    string splitStr_Default = splitStr["i"];

                    Dictionary<string, string> splitStr_RemoveX = new Dictionary<string, string>(splitStr);
                    splitStr_RemoveX["x"] = "x";
                    for (; i <= n; i++)
                    {
                        splitStr["i"] = i.ToString();
                        splitStr_RemoveX["i"] = i.ToString();
                        double result;
                        if (double.TryParse(solveParenthesis_Functions(setVariable(formula_Obj, splitStr["x"], splitStr_RemoveX), formula_Obj, false, false, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                            x += result;
                        else return ("Error", false, false);

                    }
                    splitStr["i"] = splitStr_Default;
                    splitStr_RemoveX["i"] = splitStr_Default;
                    return (x.ToString(), false, false);
                }

                else if (iParsed)
                    return (new StringVue().replaceData(splitStr["x"], "i", i.ToString()).getNewData, false, false);
                else return ("Error", false, false);

            }


        }




        private string dPlace(string value) {
            string result = "0";
            int indexE = value.IndexOf("E-");
            int indexD = value.IndexOf('.');
            if (indexE >= 0)
            {
                value = value.Remove(0,indexE+2);
                return value;
            }
            else if (indexD>=0)
            {
                value = value.Remove(0, indexD+1);
                result = value.Length.ToString();
                return result;
            }
            return result;
        }

        //create newline from tan variables
        //private int tanSwitchCount = -1;
        //public static Dictionary<int, List<bool>> tanCountIndex_AndIsSwitch_Dictionary = new Dictionary<int, List<bool>>();//bool do once, bool2 createnewline in graph


        private string solveFunctions_Default(Formula formula_Obj, string functionExpression, string functionName, bool assignRearrange_OnMainThread)
        {

            functionExpression = solveParenthesis_Functions(functionExpression, formula_Obj, true, true, assignRearrange_OnMainThread);
            functionExpression = solveBasicEquation(functionExpression);

            if (!isNumber(functionExpression))
                return functionName + "(" + functionExpression + ")";
            //FunctionLS().defaultFunctionList["functionName"];
            if (functionExpression.Contains(","))
                return functionExpression;


            //if (functionExpression.Contains("E+") || functionExpression.Contains("E-"))
            //{
            //    functionExpression = functionExpression.Contains("E+") ? replaceE_With0(functionExpression, "E+") : replaceE_With0(functionExpression, "E-");
            //}


            string result = "";
            if (functionName.Equals("dPlace", StringComparison.CurrentCulture))
            {
                result = dPlace(functionExpression);
            }
            else if (functionName.Equals("sin", StringComparison.CurrentCulture))
            {
                if (Double.Parse(functionExpression) % (180d * Math.PI / 180) == 0)
                    result = "0";
                else
                    result = Math.Sin(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("cos", StringComparison.CurrentCulture))
            {
                if (Double.Parse(functionExpression) / (90d * Math.PI / 180) % 2 == 1)
                    result = "0";
                else
                    result = Math.Cos(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("tan", StringComparison.CurrentCulture))
            {


                if (formula_Obj == null)
                {
                    double asymptoteValue = Double.Parse(functionExpression) / (Math.PI / 2.0d) % 2.0d;

                    if (asymptoteValue == 1d || asymptoteValue == -1d)
                    {
                        //throw new Exception("Asymptote Error");
                        return result = "Asymptote Error";
                    }
                    else
                    {
                        result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    }
                }
                else if (formula_Obj.comboBox_AsymptoteError_NoAsymptoteError == 1)
                    result = Math.Tan(Double.Parse(functionExpression)).ToString();
                else if (formula_Obj.comboBox_AsymptoteError_NoAsymptoteError == 0)
                {
                    //tanSwitchCount++;
                    double asymptoteValue = Double.Parse(functionExpression) / (Math.PI / 2.0d) % 2.0d;

                    if (asymptoteValue == 1d || asymptoteValue == -1d)
                    {
                        //MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { false, true };
                        //throw new Exception("Asymptote Error");
                        return result = "Asymptote Error";
                    }
                    //else if (asymptoteValue > 1d || asymptoteValue < -1d)
                    //{
                    //    if (MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary.ContainsKey(tanSwitchCount))
                    //    {
                    //        if (MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount][0] == false)
                    //        {
                    //            MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { true, true };
                    //            result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    //        }
                    //        else
                    //        {
                    //            MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { true, false };
                    //            result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { false, false };
                    //        result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    //    }
                    //}
                    //else if((asymptoteValue >= 0 && asymptoteValue < 1d)||
                    //    (asymptoteValue < 0 && asymptoteValue > -1d))
                    //{
                    //    if (MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary.ContainsKey(tanSwitchCount))
                    //    {
                    //        if (MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount][0] == true)
                    //        {
                    //            MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { false, false };
                    //            result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    //        }
                    //        else
                    //        {
                    //            MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { false, false };
                    //            result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { false, false };
                    //        result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    //    }
                    //}
                    else
                    {
                        //MathVue<double>.tanCountIndex_AndIsSwitch_Dictionary[tanSwitchCount] = new List<bool>() { false, false };
                        result = Math.Tan(Double.Parse(functionExpression)).ToString();
                    }

                }
            }





            else if (functionName.Equals("sinh", StringComparison.CurrentCulture))
            {
                result = Math.Sinh(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("cosh", StringComparison.CurrentCulture))
            {
                result = Math.Cosh(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("tanh", StringComparison.CurrentCulture))
            {
                result = Math.Tanh(Double.Parse(functionExpression)).ToString();
            }





            else if (functionName.Equals("asin", StringComparison.CurrentCulture))
            {
                result = Math.Asin(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("acos", StringComparison.CurrentCulture))
            {
                result = Math.Acos(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("atan", StringComparison.CurrentCulture))
            {
                result = Math.Atan(Double.Parse(functionExpression)).ToString();
            }





            else if (functionName.Equals("asinh", StringComparison.CurrentCulture))
            {
                result = Math.Asinh(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("acosh", StringComparison.CurrentCulture))
            {
                result = Math.Acosh(Double.Parse(functionExpression)).ToString();
            }
            else if (functionName.Equals("atanh", StringComparison.CurrentCulture))
            {
                result = Math.Atanh(Double.Parse(functionExpression)).ToString();
            }

            else if (functionName.Equals("log", StringComparison.CurrentCulture))
            {
                result = Math.Log(Double.Parse(functionExpression), 10d).ToString();
            }
            else if (functionName.Equals("ln", StringComparison.CurrentCulture))
            {
                result = Math.Log(Double.Parse(functionExpression), Math.E).ToString();
            }
            else if (functionName.Equals("abs", StringComparison.CurrentCulture))
            {
                result = Math.Abs(Double.Parse(functionExpression)).ToString();
            }

            else if (functionName.Equals("ceiling", StringComparison.CurrentCulture))
            {
                result = Math.Ceiling(Double.Parse(functionExpression)).ToString();
            }

            else if (functionName.Equals("floor", StringComparison.CurrentCulture))
            {
                result = Math.Floor(Double.Parse(solveBasicEquation(functionExpression))).ToString();
            }

            else if (functionName.Equals("sqrt", StringComparison.CurrentCulture))
            {
                result = Math.Sqrt(Double.Parse(solveBasicEquation(functionExpression))).ToString();
            }

            else if (functionName.Equals("fact!", StringComparison.CurrentCulture))
            {
                result = factorial(solveBasicEquation(functionExpression));
            }

            return result;
        }

        private string solveParenthesis_Functions(string formula, Formula formula_Obj, bool solveFunction = false, bool solveParenthesis = false, bool assignRearrange_OnMainThread = true)
        {
            string result = formula;
            int formulaLength = formula.Length;
            int first = formulaLength;
            int endFunction = -1;
            int last = -1;
            //bool isNewParenthesis = true;
            //1: solve parenthesis
            while (first > 0)
            {
                //if (first < formula.Length)
                //    first++;
                formulaLength = formula.Length;
                first = new StringVue().lastIndexOf(formula, "(", first - 1);
                //if (last == -1)
                //{
                //    last = first;
                //}
                last = new StringVue().indexOf(formula, ")", first, true);
                if (first < 0) break;
                int count = 1;

                while (first - count >= 0)
                {
                    if (isOperator(formula[first - count]) || isParenthesis(formula[first - count]) || isComma(formula[first - count]))
                    {
                        first = first - count + 1;
                        break;
                    }
                    count++;
                    if (first - count < 0)
                    {
                        first = first - count + 1;
                        break;
                    }
                }
                // beginningFunc = first;

                endFunction = first + count - 1;
                //parenthesis substring
                if (last - endFunction - 1 < 0) return "Error";
                string parSubStr = formula.Substring(first + count, last - endFunction - 1);
                string functionName = formula.Substring(first, endFunction - first);
                //Debug.WriteLine("FormulaToSolve: " + formula);
                //Debug.WriteLine("first: " + first);
                //Debug.WriteLine("endFunction: " + endFunction);
                //Debug.WriteLine("Function name: " + functionName);
                //solve Parenthesis


                if (endFunction - first == 0)
                {
                    string temp = parSubStr;
                    if (!solveParenthesis)
                        if (solveFunction)
                        {
                            if (!isFunctionTo_SolveFirst_isAddPar.ContainsKey(functionName))
                                result = "(" + solveFunctions_Custom(temp, functionName, formula_Obj, solveFunction) + ")";
                            else
                                result = solveFunctions_Custom(temp, functionName, formula_Obj, solveFunction);
                        }
                        else
                        {
                            if (!isFunctionTo_SolveFirst_isAddPar.ContainsKey(functionName))
                                result = "(" + solveFunctions_Custom(temp, functionName, formula_Obj, false) + ")";
                            else
                                result = solveFunctions_Custom(temp, functionName, formula_Obj, false);
                        }
                    else
                        result = solveBasicEquation(temp, functionName);
                    //if (containsErrorSkip(result)) return result;
                    //result = solveBasicEquation(parSubStr);
                    result = formula = new StringVue().replace(formula, endFunction, last - first + count, result);
                    //Debug.WriteLine("result: " + result);
                    //Debug.WriteLine("formula: " + formula);
                }
                else
                {
                    //solve functions
                    string temp = parSubStr;
                    //result = solveFunctions_Custom(parSubStr, functionName, formula_Obj);
                    if (!solveParenthesis)
                        if (solveFunction)
                        {
                            if (!isFunctionTo_SolveFirst_isAddPar.ContainsKey(functionName))
                                result = "(" + solveFunctions_Custom(temp, functionName, formula_Obj, solveFunction) + ")";
                            else
                                result = solveFunctions_Custom(temp, functionName, formula_Obj, solveFunction);

                        }
                        else
                        {
                            if (!isFunctionTo_SolveFirst_isAddPar.ContainsKey(functionName))
                                result = "(" + solveFunctions_Custom(temp, functionName, formula_Obj, false) + ")";
                            else
                                result = solveFunctions_Custom(temp, functionName, formula_Obj, false);
                        }
                    else
                        result = solveBasicEquation(temp, functionName);
                    //if (containsErrorSkip(result)) return result;
                    //if (temp.Equals(result))
                    //result = solveFunctions_Default(parSubStr, functionName);
                    //result = formula = new StringVue().replace(formula, first, last - first + 1, result);
                    result = formula = new StringVue().replace(formula, first, last - first + 1, result);

                    //Debug.WriteLine("test: " + first);
                }

            }
            //2: solve non parenthesis


            if (!solveFunction)
            {
                //if (formula_Obj != null)
                    //if (formula_Obj.variableList_Bind.Count > 0)
                        //formula = setVariable(formula_Obj, formula);
                if (assignRearrange_OnMainThread)
                    formula_Obj.rearrangedFormula = formula;
                result = formula = solveParenthesis_Functions(formula, formula_Obj, true, false, assignRearrange_OnMainThread);
                if (containsErrorSkip(result).isError) return result = containsErrorSkip(result).errorString;
                else
                    result = formula = solveParenthesis_Functions(formula, formula_Obj, true, true, assignRearrange_OnMainThread);
                if (containsErrorSkip(result).isError) return result = containsErrorSkip(result).errorString;
                else
                    result = solveBasicEquation(formula);
            }
            if (solveParenthesis)
            {
                //formula = setVariable(formula_Obj,formula,null,true);
                if (containsErrorSkip(result).isError) return result = containsErrorSkip(result).errorString;
                else
                    result = solveBasicEquation(formula);
            }
            //result = solveBasicEquation(formula);
            return result;
        }

    }
}
