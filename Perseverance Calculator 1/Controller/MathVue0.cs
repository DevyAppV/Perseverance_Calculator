using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perseverance_Calculator_1.Controller
{
    internal partial class MathVue<T>
    {
        public string rearrangeFormula_Solve4SingleVar(string formula, Formula formula_Obj = null)
        {

            string formula_ToRearrange = "";

            if (!string.IsNullOrEmpty(formula))
                formula_ToRearrange = formula;
            else formula_ToRearrange = formula_Obj.formula;

            //getLeftSide
            //getRightSide
            List<(List<(string variable, string leftOperation, string rightOperation, string parenthesis)> variable, string side)> varList_Sides = getVarList_Sides(formula_ToRearrange, formula_Obj);

            return "";
        }


        public List<(List<(string variable, string leftOperation, string rightOperation, string parenthesis)> variable, string side)> getVarList_Sides(string formula_ToRearrange, Formula formula_Obj=null)
        {
            int currentSide = 1;
            List<(List<(string variable, string leftOperation, string rightOperation, string parenthesis)> variable, string side)> result = 
                new List<(List<(string variable, string leftOperation, string rightOperation, string parenthesis)> variable, string side)>() { (new List<(string variable, string leftOperation, string rightOperation, string parenthesis)>(), "side" + currentSide.ToString()) };


            string variable = "";
            string previousOperation = "";


            string varToSolve = "";
            foreach (var v in formula_Obj.variableList_Bind)
            {
                if (v.value.Equals(v.name))
                {
                    varToSolve = v.name;
                    break;
                }
            }

            bool isPar = false;
            string parStr = "";
            string varToSolveSide = "";
            for (int i = 0; i < formula_ToRearrange.Length; i++)
            {
                while (isParenthesis(formula_ToRearrange[i]))
                {
                    isPar = true;
                    parStr += formula_ToRearrange[i];
                    i++;

                }
                if (isPar)
                {
                    if (!string.IsNullOrWhiteSpace(variable))
                    {
                        if (variable.Equals(varToSolve))
                        {
                            //result = new List<(List<(string variable, string leftOperation, string rightOperation, string parenthesis)> variable, string side)>() ;
                            //result[0].varToSolveSide = Tuple.Create(item1:"");
                            varToSolveSide = "side" + currentSide.ToString();
                        }
                        variable = "";
                    }
                    result[result.Count - 1].variable.Add(("", "", "", parStr));
                    parStr = "";
                    isPar = false;
                }
                if (isOperator(formula_ToRearrange[i]))
                {
                    result[result.Count - 1].variable.Add((variable, previousOperation, formula_ToRearrange[i].ToString(),""));
                    variable = "";
                    previousOperation = formula_ToRearrange[i].ToString();
                }
                else if (formula_ToRearrange[i].Equals('='))
                {

                    result[result.Count - 1].variable.Add((variable, previousOperation, formula_ToRearrange[i].ToString(), ""));
                    currentSide++;

                    result = new List<(List<(string variable, string leftOperation, string rightOperation, string parenthesis)> variable, string side)>()
                    {
                        (new List<(string variable, string leftOperation, string rightOperation, string parenthesis)>(), "side"+ currentSide.ToString())
                    };
                    variable = "";
                    previousOperation = formula_ToRearrange[i].ToString();

                }
                else
                    variable += formula_ToRearrange[i];
            }

            return result;
        }

        Dictionary<int, List<string>> rules = new Dictionary<int, List<string>>() {

            { 0, new List<string>(){ "(",")","[","]" } },
            { 1, new List<string>(){ "+","-" } },
            { 2, new List<string>(){ "*" } },
            { 3, new List<string>(){ "/" } },
            //{ 4, new List<string>(){ "%" } },
            { 4, new List<string>(){ "^" } },

        };
        private string rearrangeOperation(List<(List<(string variable, string leftOperation, string rightOperation)> variable, string side)> varList_Sides, Formula formula_Obj)
        {

            string result = "";



            for (int side = 0; side < varList_Sides.Count; side++)
            {
                for (int variable = 0; variable < varList_Sides[side].variable.Count; variable++)
                {
                    foreach (KeyValuePair<int, List<string>> rule in rules)
                    {
                        foreach (string ruleVal in rule.Value)
                        {
                            if (varList_Sides[side].variable[variable].leftOperation.Equals(""))
                            {

                                if (varList_Sides[side].variable[variable].rightOperation.Equals(ruleVal))
                                {
                                }
                            }
                            else if (varList_Sides[side].variable[variable].leftOperation.Equals(""))
                            {

                            }
                        }
                    }
                }

            }

            return result;
        }
    }
}
