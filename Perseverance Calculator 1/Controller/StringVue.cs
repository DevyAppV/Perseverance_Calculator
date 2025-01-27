using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perseverance_Calculator_1.Controller
{
    internal class StringVue
    {
        public string replace(string mainString, int startIndex, int count, string newString)
        {
            string result = mainString;
            result = result.Remove(startIndex, count).Insert(startIndex, newString);
            return result;
        }

        private int IsInIgnoreFunctionList(int index, Dictionary<string, List<int>> ignoreList)
        {
            foreach (KeyValuePair<string, List<int>> i in ignoreList)
            {
                for (int q = 0; q < i.Value.Count; q += 2)
                {
                    if (index >= i.Value[q] && index <= i.Value[q + 1])
                    {
                        return i.Value[q + 1];
                    }
                }
            }
            return -1;
        }

        public Dictionary<string, string> splitString_BaseOnComma(string strToSplit, Dictionary<string, string> splitStr_UseKey = null, Dictionary<string, List<int>> ignoreList = null)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            int index = 0;
            int lastIndex = 0;
            int ignore = 0;
            bool foundComma = false;
            int splitStr_UseKey_CurrentElement = 0;
            int indexBeforeSplit = -1;
            while (indexOf(strToSplit, ",", index) != -1)
            {
                if (index != ignore)
                    lastIndex = index;
                index = indexOf(strToSplit, ",", index);
                if (ignoreList != null)
                {
                    ignore = IsInIgnoreFunctionList(index, ignoreList);
                    if (ignore >= 0)
                    {
                        index = ignore;
                        continue;
                    }
                    else
                    {
                        foundComma = true;
                        indexBeforeSplit = index;
                    }
                }
                else
                {
                    foundComma = true;
                }
                if (splitStr_UseKey == null)
                {
                    result.Add(strToSplit.Substring(lastIndex, index - lastIndex), strToSplit.Substring(lastIndex, index - lastIndex));
                }
                else
                {
                    result.Add(splitStr_UseKey.ElementAt(splitStr_UseKey_CurrentElement).Key, strToSplit.Substring(lastIndex, index - lastIndex).ToString());
                    splitStr_UseKey_CurrentElement++;
                }
                index++;
            }
            if (foundComma)
            {
                if (splitStr_UseKey == null)
                {
                    if (indexBeforeSplit >= 0)
                        result.Add(strToSplit.Substring(indexBeforeSplit + 1), strToSplit.Substring(indexBeforeSplit + 1));
                    else
                        result.Add(strToSplit.Substring(lastIndexOf(strToSplit, ",") + 1), strToSplit.Substring(lastIndexOf(strToSplit, ",") + 1));
                }
                else
                {
                    if (splitStr_UseKey_CurrentElement >= splitStr_UseKey.Count)
                        return null;
                    if (indexBeforeSplit >= 0)
                        result.Add(splitStr_UseKey.ElementAt(splitStr_UseKey_CurrentElement).Key, strToSplit.Substring(indexBeforeSplit + 1));
                    else
                        result.Add(splitStr_UseKey.ElementAt(splitStr_UseKey_CurrentElement).Key, strToSplit.Substring(lastIndexOf(strToSplit, ",") + 1));
                    splitStr_UseKey_CurrentElement++;
                }
            }
            else if (result.Count == 0 && !foundComma)
                if (splitStr_UseKey != null && splitStr_UseKey.Count > 0)
                    result.Add(splitStr_UseKey.ElementAt(splitStr_UseKey_CurrentElement).Key, strToSplit);
                else
                    result.Add(strToSplit, strToSplit);
            return result;
        }


        public (int, string) getString_FuntionComma(string mainString, int startIndex = 0)
        {
            string result = mainString;
            int resultIndex = -1;
            int count = startIndex;
            int mainStringLength = mainString.Length;
            while (count < mainStringLength)
            {
                if (result[count] == ',')
                {
                    result = result.Substring(startIndex, count - startIndex);
                    resultIndex = count;
                    break;
                }
                count++;
                if (count >= mainStringLength)
                {
                    result = result.Substring(startIndex, count - startIndex);
                    resultIndex = count;
                }
            }
            //index at ",", result =substring
            return (resultIndex, result);
        }

        public int indexOf(string mainString, string strToChk, int startIndex = 0, bool checkAgainstOpenPar = false)
        {
            int result = -1;
            int mainStringLength = mainString.Length;
            int strToChkLength = strToChk.Length;
            int countOpenPar = 0;
            if (startIndex == -1) return result;
            //string chkResult = "";
            for (int i = startIndex; i < mainStringLength; i++)
            {
                if (checkAgainstOpenPar)
                {
                    //if (strToChk.Equals(")"))
                    //{
                    if (mainString[i].Equals('('))
                        countOpenPar++;
                    else if (mainString[i].Equals(')'))
                        countOpenPar--;
                    //}
                }
                if (mainString[i].Equals(strToChk[0]))
                {
                    result = i;
                    if (strToChkLength == 1 && countOpenPar < 0)
                        break;
                    int count = 1;
                    while (i < mainStringLength && strToChkLength != 1 && count < strToChkLength
                        && mainString[i] == strToChk[count - 1])
                    {
                        //if (countOpenPar <= 0)
                        //{
                        //    //result = i;
                        //    return i;
                        //}
                        countOpenPar--;
                        i++;
                        count++;
                    }
                    i++;
                    while (i < mainStringLength && strToChkLength == 1
                        && mainString[i] == strToChk[0])
                    {
                        if (countOpenPar <= 0)
                        {
                            //result = i;
                            return --i;
                        }
                        countOpenPar--;
                        i++;
                        count++;
                    }
                    i--;
                    if (count == strToChkLength && countOpenPar <= 0)
                        break;
                    else if (count != strToChkLength && !checkAgainstOpenPar)
                    {
                        result = -1;//COMMENT IF ERROR
                    }
                    else if (countOpenPar > 0)
                    {
                        //countOpenPar--;
                        continue;
                    }
                    else if (countOpenPar <= 0)
                    {
                        if (countOpenPar <= 0)
                        {
                            //result = i;
                            return i;
                        }
                        //countOpenPar--;
                        result = i;
                    }
                    else
                    {
                        result = -1;

                    }
                }

            }
            return result;
        }
        public int lastIndexOf(string mainString, string strToChk, int startIndex = -1)
        {
            int result = -1;
            int mainStringLength = mainString.Length;
            int strToChkLength = strToChk.Length;
            //string chkResult = "";

            if (startIndex < 0)
                startIndex = mainStringLength - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (mainString[i] == strToChk[strToChkLength - 1])
                {
                    result = i;
                    if (strToChkLength == 1)
                        break;
                    i--;
                    int count = 1;
                    while (i >= 0 && count < strToChkLength
                        && mainString[i] == strToChk[strToChkLength - 1 - count])
                    {
                        i--;
                        count++;
                    }
                    if (count == strToChkLength)
                    {
                        result = i + 1;
                        break;
                    }
                    else
                        result = -1;
                }

            }
            return result;
        }


















        public string replaceFormulaFunction(string mainStr, string functionToReplace = "formula(")
        {

            //string functionToReplace = "formula(";

            if (!string.IsNullOrWhiteSpace(mainStr))
            {

                //string result = mainStr;
                int indexOfTime = 0;
                string subStrExpression = "";
                string subStrFormulaName = "";
                int functionComma = -1;
                int functionEndPar = -1;

                MathVue<decimal> mv = new MathVue<decimal>();
                //try
                //{
                while (mainStr.IndexOf(functionToReplace, indexOfTime, StringComparison.CurrentCulture) > -1)
                {
                    indexOfTime = mainStr.IndexOf(functionToReplace, StringComparison.CurrentCulture);
                    functionComma = indexOf(mainStr, ",", indexOfTime + functionToReplace.Length, true);
                    functionEndPar = indexOf(mainStr, ")", indexOfTime, true);

                    bool found = false;
                    if (indexOfTime - 1 >= 0)
                    {
                        if ((
                            mv.isOperator(mainStr[indexOfTime - 1]) ||
                            mv.isComma(mainStr[indexOfTime - 1]) ||
                            mv.isParenthesis(mainStr[indexOfTime - 1])
                            ))
                        {
                            found = true;
                            subStrFormulaName = mainStr.Substring(indexOfTime + functionToReplace.Length, (functionComma - 1) - (indexOfTime + functionToReplace.Length - 1));
                            subStrExpression = mainStr.Substring(functionComma + 1, functionEndPar - (functionComma + 1));
                            mainStr = mainStr.Remove(indexOfTime, functionEndPar + 1);
                            mainStr = replaceData(mainStr, subStrFormulaName, subStrExpression).getNewData;
                        }

                    }
                    else
                    {
                        found = true;
                        subStrFormulaName = mainStr.Substring(indexOfTime + functionToReplace.Length, (functionComma - 1) - (indexOfTime + functionToReplace.Length - 1));
                        subStrExpression = mainStr.Substring(functionComma + 1, functionEndPar - (functionComma + 1));
                        mainStr = mainStr.Remove(indexOfTime, functionEndPar + 1);
                        mainStr = replaceData(mainStr, subStrFormulaName, subStrExpression).getNewData;
                    }
                    if (!found)
                        indexOfTime++;
                }
                return mainStr;
                //}
                //catch { }
                //return "";
            }
            else

                return "";


        }

        public (string getNewData, bool containsDataToReplace) replaceData(string data, string dataToReplace, string newData, bool decrementIfDataToReplace_IsFound=false)
        {

            //throw new Exception();
            //}
            bool containsDataToReplace = false;
            if (!string.IsNullOrEmpty(data))
            {
                int indexOfTime = data.Length - 1;
                MathVue<decimal> mv = new MathVue<decimal>();
                //try
                //{
                if (!string.IsNullOrWhiteSpace(data))
                {
                    while (indexOfTime>=0 && data.LastIndexOf(dataToReplace, indexOfTime, StringComparison.CurrentCulture) > -1)
                    {
                        bool found = false;
                        indexOfTime = data.LastIndexOf(dataToReplace, indexOfTime, StringComparison.CurrentCulture);
                        if (indexOfTime - 1 >= 0 && indexOfTime + dataToReplace.Length <= data.Length - 1)
                        {
                            if ((
                                mv.isOperator(data[indexOfTime - 1]) ||
                                mv.isComma(data[indexOfTime - 1]) ||
                                mv.isParenthesis(data[indexOfTime - 1])
                                ) &&
                                (
                                mv.isOperator(data[indexOfTime + dataToReplace.Length]) ||
                                mv.isComma(data[indexOfTime + dataToReplace.Length]) ||
                                mv.isParenthesis(data[indexOfTime + dataToReplace.Length])
                                ))
                            {
                                found = true;
                                data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData);
                                if (!containsDataToReplace) containsDataToReplace = true;
                            }
                        }
                        else if (indexOfTime - 1 >= 0 && indexOfTime + dataToReplace.Length > data.Length - 1)
                        {
                            if ((
                                mv.isOperator(data[indexOfTime - 1]) ||
                                mv.isComma(data[indexOfTime - 1]) ||
                                mv.isParenthesis(data[indexOfTime - 1])
                                ))
                            {
                                found = true;
                                data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData);
                                if (!containsDataToReplace) containsDataToReplace = true;
                            }

                        }
                        else if (indexOfTime - 1 < 0 && indexOfTime + dataToReplace.Length <= data.Length - 1)
                        {
                            if (
                                (
                                mv.isOperator(data[indexOfTime + dataToReplace.Length]) ||
                                mv.isComma(data[indexOfTime + dataToReplace.Length]) ||
                                mv.isParenthesis(data[indexOfTime + dataToReplace.Length])
                                ))
                            {
                                found = true;
                                data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData);
                                if (!containsDataToReplace) containsDataToReplace = true;
                            }

                        }
                        else if (indexOfTime - 1 < 0 && indexOfTime + dataToReplace.Length > data.Length - 1)
                        {
                            found = true;
                            data = data.Remove(indexOfTime, dataToReplace.Length).Insert(indexOfTime, newData);
                            if (!containsDataToReplace) containsDataToReplace = true;

                        }
                        if (!found)
                            indexOfTime--;
                        else if(decrementIfDataToReplace_IsFound && found)
                            indexOfTime--;
                        if (containsDataToReplace && dataToReplace.Equals(newData)) return (data, true);
                    }
                    return (data, containsDataToReplace);
                }
                //}
                //catch { }
            }
            return ("", containsDataToReplace);
        }
    }
}
