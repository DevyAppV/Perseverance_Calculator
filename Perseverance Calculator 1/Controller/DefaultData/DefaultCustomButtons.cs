using Perseverance_Calculator_1.Model.MVVM;
using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using System.Diagnostics;
using System.Drawing;
using Microsoft.UI.Xaml.Media;
using System.Reflection.Metadata;

namespace Perseverance_Calculator_1.Controller.DefaultData
{
    public class DefaultCustomButtons
    {



        public static async void populateButtonWith_DefaultProgram()
        {
            int indexOfDefaultTab = -1;
            bool found = false;

            if (FunctionCollectionTab_MVVM.functionCollectionTab != null)
            {
                for (int i = 0; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                {
                    bool breakOut = false;
                    await Task.Run(() =>
                    {
                        if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Program", StringComparison.CurrentCulture))
                        {
                            for (int j = 0; j < FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind.Count; j++)
                            {
                                if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(
                                    FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind[j].formulaObj.formulaName))
                                {
                                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Remove(
                                        FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind[j].formulaObj.formulaName);
                                }
                            }
                            App.m_window.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind.Clear();
                                breakOut = true;
                            });
                            indexOfDefaultTab = i;
                            found = true;
                            //break;
                        }
                    });
                    if (breakOut) { break; }
                }
            }
            if (!found)
            {
                FunctionCollection defaultCollectionTab = new FunctionCollection() { functionTabName = "Program" };
                FunctionCollectionTab_MVVM.functionCollectionTab.Add(defaultCollectionTab);
                indexOfDefaultTab = FunctionCollectionTab_MVVM.functionCollectionTab.IndexOf(defaultCollectionTab);
            }

            ////-------------------------Constants
            ////-------------------------Constants
            ////-------------------------Constants
            ////-------------------------Constants
            //all graphs = blue //null
            //1d graphs = green //#FF0EFF0E
            //2d graphs = orange //#FFFF6900
            //3d graphs = yellow //#FFFFEA00



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("formula"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(
                    new Formula()
                    {
                        formulaName = "formula",//formula name
                        formula = "formula(formulaName,x)",//formula
                        rearrangedFormula = "formula(formulaName,x)",//formula rearranged
                        description = "Acts as a variable for an expression.  This will replace all 'formulaName' with the expression 'x' during calculations.",//formula description

                        variableList_Bind = new ObservableCollection<VariableData> {

                        new VariableData(
                        //"x",//variable
                        "formulaName",//variable name
                        "formulaName",//variable value
                        "name of formula"),
                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "formula expression")},
                    },
                    use: "formula(formulaName,x)"//formula formula use

                    ////variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }







            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("refUpdate"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "refUpdate",//formula name
                    formula = "refUpdate(i,n,ref,x)",//formula
                    rearrangedFormula = "refUpdate(i,n,ref,x)",//formula rearranged
                    description = "Loop from i to n while updating ref by x.  The variable ref must be included in the expression x",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> {

                        new VariableData(
                        //"x",//variable
                        "i",//variable name
                        "i",//variable value
                        "startIndex; lower than n"),
                        new VariableData(
                        //"x",//variable
                        "n",//variable name
                        "n",//variable value
                        "endIndex; greater than or equal to n"),
                        new VariableData(
                        //"x",//variable
                        "ref",//variable name
                        "ref",//variable value
                        "the initial value to be updated; can include i"),
                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "formula expression; must include ref; can include i"),

                    },
                },
                    use: "refUpdate(i,n,ref,x)"//formula formula use


                    ////variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }









            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("ifThen"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "ifThen",//formula name
                    formula = "ifThen(x,rOperator,y,trueReturn,falseReturn)",//formula
                    rearrangedFormula = "ifThen(x,rOperator,y,trueReturn,falseReturn)",//formula rearranged
                    description = "If x compared to y is true then return a value (trueReturn) else false then return a value (falseReturn); ",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> {

                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "The first expression; can be an expression, 'true' or 'false'"),
                        new VariableData(
                        //"x",//variable
                        "rOperator",//variable name
                        "rOperator",//variable value
                        "Relational Operator (>,<,>=,<=,!=,==); For true and false comparison use either (==, or !=)"),//variable description,
                        new VariableData(
                        //"x",//variable
                        "y",//variable name
                        "y",//variable value
                        "The second expression: can be an expression, 'true' or 'false'"),//variable description,
                        new VariableData(
                        //"x",//variable
                        "trueReturn",//variable name
                        "trueReturn",//variable value
                        "Can set this as a new expression to be calculated, or set this as 'true' or 'false' to be compared again within another another function that accepts true or false. " +
                        "Can set this as 'throw' followed by an error message to cause an error."),//variable description,
                        new VariableData(
                        //"x",//variable
                        "falseReturn",//variable name
                        "falseReturn",//variable value
                        "Can set this as a new expression to be calculated, or set this as 'true' or 'false' to be compared again within another another function that accepts true or false. "+
                        "Can set this as 'throw' followed by an error message to cause an error."),//variable description

                    },
                },
                    use: "ifThen(x,rOperator,y,trueReturn,falseReturn)"//formula formula use


                    //
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }




            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("orThen"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "orThen",//formula name
                    formula = "orThen(x,y,trueReturn,falseReturn)",//formula
                    rearrangedFormula = "orThen(x,y,trueReturn,falseReturn)",//formula rearranged
                    description = "If x is true or y is true then return a value (trueReturn) else false then return a value (falseReturn)",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> {

                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "The first expression; can 'true' or 'false'; can use ifThen to return true or false;"),
                        new VariableData(
                        //"x",//variable
                        "y",//variable name
                        "y",//variable value
                        "The second expression: can 'true' or 'false'; can use ifThen to return true or false;"),//variable description,,
                        new VariableData(
                        //"x",//variable
                        "trueReturn",//variable name
                        "trueReturn",//variable value
                        "Can set this as a ,new expression to be calculated, or set this as 'true' or 'false' to be compared again within another function that accepts true or false. "+
                        "Can set this as 'throw' followed by an error message to cause an error."),//variable description

                        new VariableData(
                        //"x",//variable
                        "falseReturn",//variable name
                        "falseReturn",//variable value
                        "Can set this as a new expression to be calculated, or set this as 'true' or 'false' to be compared again within another function that accepts true or false. " +
                        "Can set this as 'throw' followed by an error message to cause an error."),//variable description
                    },
                },
                    use: "orThen(x,y,trueReturn,falseReturn)"//formula formula use


                    //functionTab_IndexLocation: 
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }




            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("dPlace"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "dPlace",//formula name
                    formula = "dPlace(x)",//formula
                    rearrangedFormula = "dPlace(x)",//formula rearranged
                    description = "Returns the number of decimal places.",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> {

                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "A decimal number or expression"),
                    },
                },
                    use: "dPlace(x)"//formula formula use


                    //functionTab_IndexLocation: 
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            //-------------------------3D Graph
            //-------------------------3D Graph
            //-------------------------3D Graph
            //-------------------------3D Graph



            //TESTING

            //FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
            //    new CustomFunction(
            //        "timex",//formula name
            //        "timex(x,y)",//formula
            //        "timex(x,y)",//formula rearranged
            //        "timex(x,y)",//formula formula use
            //        "test",//formula description
            //        new ObservableCollection<VariableData>
            //        {
            //            new VariableData(
            //            //"x",//variable
            //            "x",//variable name
            //            "x",//variable value
            //            ""),
            //            new VariableData(
            //            //"x",//variable
            //            "y",//variable name
            //            "y",//variable value
            //            "")
            //        },//variable description
            //        ignoreFunctionForGraph: true
            //        ));
            //index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
            //FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
            //    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
            //    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);






        }





        public static async void populateButtonWith_DefaultGraph()
        {
            int indexOfDefaultTab = -1;
            bool found = false;

            if (FunctionCollectionTab_MVVM.functionCollectionTab != null)
            {
                for (int i = 0; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                {
                    bool breakOut = false;
                    await Task.Run(() =>
                    {
                        if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Graph", StringComparison.CurrentCulture))
                        {
                            for (int j = 0; j < FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind.Count; j++)
                            {
                                if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(
                                    FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind[j].formulaObj.formulaName))
                                {
                                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Remove(
                                        FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind[j].formulaObj.formulaName);
                                }
                            }
                            App.m_window.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind.Clear();
                                breakOut = true;
                            });
                            indexOfDefaultTab = i;
                            found = true;
                            //break;
                        }
                    });
                    if (breakOut) { break; }
                }
            }
            if (!found)
            {
                FunctionCollection defaultCollectionTab = new FunctionCollection() { functionTabName = "Graph" };
                FunctionCollectionTab_MVVM.functionCollectionTab.Add(defaultCollectionTab);
                indexOfDefaultTab = FunctionCollectionTab_MVVM.functionCollectionTab.IndexOf(defaultCollectionTab);
            }

            ////-------------------------Constants
            ////-------------------------Constants
            ////-------------------------Constants
            ////-------------------------Constants
            //all graphs = blue //null
            //1d graphs = green //#FF0EFF0E
            //2d graphs = orange //#FFFF6900
            //3d graphs = yellow //#FFFFEA00
            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("time"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                    new CustomFunction(new Formula()
                    {
                        formulaName = "time",//formula name
                        formula = "time",//formula
                        rearrangedFormula = "time",//formula rearranged
                        description = "The time it takes to update the graph.  Time is used along with the play button in graphs.",//formula description
                        variableList_Bind = new ObservableCollection<VariableData>
                        {
                        },
                    },
                    "time"//formula formula use


                    ////variable description
                        ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }







            //-------------------------1D Graph
            //-------------------------1D Graph
            //-------------------------1D Graph
            //-------------------------1D Graph



            //-------------------------2D Graph
            //-------------------------2D Graph
            //-------------------------2D Graph
            //-------------------------2D Graph


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("circleRadius"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "circleRadius",//formula name
                    formula = "circleRadius",//formula
                    rearrangedFormula = "circleRadius",//formula rearranged
                    description = "circleRadius - Works only with 2D Graph; Returns the radius of a point plotted in 2d Graph.  It can be altered in the graphs' add section",//formula description

                    variableList_Bind = new ObservableCollection<VariableData>
                    {
                    },
                },
                    "circleRadius",//formula formula use

                    ////variable description
                    ignoreFunctionForGraph: false,
                    "#FFFF6900"
                    /*,*/
                    //new SolidColorBrush(Windows.UI.Color.FromArgb(
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).A,//orange
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).R,
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).G,
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).B

                    //    ))//hex is orange
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }




            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("vec2"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "vec2",//formula name
                    formula = "vec2(x,y)",//formula
                    rearrangedFormula = "vec2(x,y)",//formula rearranged
                    description = "2d vector - only works with 2D graph; can only be used by itself but only once.  It plots a point on the graph.",//formula description
                    variableList_Bind = new ObservableCollection<VariableData>
                    {
                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x position"),
                        new VariableData(
                        //"x",//variable
                        "y",//variable name
                        "y",//variable value
                        "y position")
                    },
                },
                    "vec2(x,y)",//formula formula use

                    ////variable description
                    ignoreFunctionForGraph: true,
                    "#FFFF6900"
                    /*,*/
                    //new SolidColorBrush(Windows.UI.Color.FromArgb(
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).A,//orange
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).R,
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).G,
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).B

                    //    ))//hex is orange
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("instance"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "instance",//formula name
                    formula = "instance(x,y)",//formula
                    rearrangedFormula = "instance(x,y)",//formula rearranged
                    description = "instance - only works with 2D graph; can only be used within vec2; " +
                    "'x' and 'y' must be the same value when using 'instance' multiple times in vec2 " +
                    "creates a copy of an expression starting from x to y; " +
                    "at the same time 'instance' will return the number of the current instance",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData>
                    {
                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x starting instance (integer)"),
                        new VariableData(
                        //"x",//variable
                        "y",//variable name
                        "y",//variable value
                        "y ending instance (integer greater than x)")
                    },
                },
                    "instance(x,y)",//formula formula use

                    //variable description
                    ignoreFunctionForGraph: true,
                    "#FFFF6900"
                    /*,*/
                    //new SolidColorBrush(Windows.UI.Color.FromArgb(
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).A,//orange
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).R,
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).G,
                    //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FFFF6900")).B

                    //    ))//hex is orange
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            //-------------------------3D Graph
            //-------------------------3D Graph
            //-------------------------3D Graph
            //-------------------------3D Graph



            //TESTING

            //FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
            //    new CustomFunction(
            //        "timex",//formula name
            //        "timex(x,y)",//formula
            //        "timex(x,y)",//formula rearranged
            //        "timex(x,y)",//formula formula use
            //        "test",//formula description
            //        new ObservableCollection<VariableData>
            //        {
            //            new VariableData(
            //            //"x",//variable
            //            "x",//variable name
            //            "x",//variable value
            //            ""),
            //            new VariableData(
            //            //"x",//variable
            //            "y",//variable name
            //            "y",//variable value
            //            "")
            //        },//variable description
            //        ignoreFunctionForGraph: true
            //        ));
            //index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
            //FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
            //    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
            //    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);






        }




















































        public static async void populateButtonWith_Default()
        {
            int indexOfDefaultTab = -1;
            bool found = false;

            if (FunctionCollectionTab_MVVM.functionCollectionTab != null)
            {
                for (int i = 0; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                {
                    bool breakOut = false;
                    await Task.Run(() =>
                    {
                        if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Default", StringComparison.CurrentCulture))
                        {
                            for (int j = 0; j < FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind.Count; j++)
                            {
                                if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(
                                    FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind[j].formulaObj.formulaName))
                                {
                                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Remove(
                                        FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind[j].formulaObj.formulaName);
                                }
                            }
                            App.m_window.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                breakOut = true;
                                FunctionCollectionTab_MVVM.functionCollectionTab[i].customFunctionCollection_Bind.Clear();
                            });
                            indexOfDefaultTab = i;
                            found = true;
                            //break;
                        }
                    });
                    if (breakOut) break;
                }
            }
            if (!found)
            {
                FunctionCollection defaultCollectionTab = new FunctionCollection() { functionTabName = "Default" };
                FunctionCollectionTab_MVVM.functionCollectionTab.Add(defaultCollectionTab);
                indexOfDefaultTab = FunctionCollectionTab_MVVM.functionCollectionTab.IndexOf(defaultCollectionTab);
            }







            //-------------------------Constants
            //-------------------------Constants
            //-------------------------Constants
            //-------------------------Constants

            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("π"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "π",//formula name
                    formula = Math.PI.ToString(),//formula
                    rearrangedFormula = Math.PI.ToString(),//formula rearranged
                    description = "Constant PI",//formula description
                    variableList_Bind = new ObservableCollection<VariableData>
                    {
                    },
                },
                    "π"//formula formula use
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }




            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("e"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "e",//formula name
                    formula = Math.E.ToString(),//formula
                    rearrangedFormula = Math.E.ToString(),//formula rearranged
                    description = "Constant e",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData>
                    {
                    },
                },



                    "e"//formula formula use

                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("random"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "random",//formula name
                    formula = "random(min,max)",//formula
                    rearrangedFormula = "random(min,max)",//formula rearranged
                    description = "Generate a random number >= min and < y; Includes min; Not including max",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "min",//variable name
                        "min",//variable value
                        "min is an expression; Integer;"),new VariableData(
                        //"x",//variable
                        "max",//variable name
                        "max",//variable value
                        "max is an expression; Integer;"),

                    },
                },
                    "random(min,max)"//formula formula use

                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("sqrt"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "sqrt",//formula name
                    formula = "sqrt(x)",//formula
                    rearrangedFormula = "sqrt(x)",//formula rearranged
                    description = "square root of a number",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x is a number")},
                },



                    "sqrt(x)"//formula formula use


                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }

            //-----------------ATan2
            //-----------------ATan2
            //-----------------ATan2
            //-----------------ATan2


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("atan2"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "atan2",//formula name
                    formula = "atan2(x,y)",//formula
                    rearrangedFormula = "atan2(x,y)",//formula rearranged
                    description = "Select a point (x,y) on a circle and returns the degree.  0 degree starts at 90 degrees",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x"),
                    new VariableData(
                        //"x",//variable
                        "y",//variable name
                        "y",//variable value
                        "y")
                    },
                },



                    "atan2(x,y)"//formula formula use

                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }





            //-----------------SINCOSTAN
            //-----------------SINCOSTAN
            //-----------------SINCOSTAN
            //-----------------SINCOSTAN


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("sin"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "sin",//formula name
                    formula = "sin(x)",//formula
                    rearrangedFormula = "sin(x)",//formula rearranged
                    description = "sin of an angle",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "sin(x)"//formula formula use


                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("cos"))
            {

                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "cos",//formula name
                    formula = "cos(x)",//formula
                    rearrangedFormula = "cos(x)",//formula rearranged
                    description = "cos of an angle",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "cos(x)"//formula formula use


                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("tan"))
            {

                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "tan",//formula name
                    formula = "tan(x)",//formula
                    rearrangedFormula = "tan(x)",//formula rearranged
                    description = "tan of an angle",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "tan(x)"//formula formula use


                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }

            //-----------------SINCOSTAN H
            //-----------------SINCOSTAN H
            //-----------------SINCOSTAN H
            //-----------------SINCOSTAN H

            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("sinh"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "sinh",//formula name
                    formula = "sinh(x)",//formula
                    rearrangedFormula = "sinh(x)",//formula rearranged
                    description = "sinh of an angle",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "sinh(x)"//formula formula use


                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("cosh"))
            {

                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "cosh",//formula name
                    formula = "cosh(x)",//formula
                    rearrangedFormula = "cosh(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "cosh(x)"//formula formula use

                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("tanh"))
            {

                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "tanh",//formula name
                    formula = "tanh(x)",//formula
                    rearrangedFormula = "tanh(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "tanh(x)"//formula formula use

                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }







            //-----------------ARC SINCOSTAN
            //-----------------ARC SINCOSTAN
            //-----------------ARC SINCOSTAN
            //-----------------ARC SINCOSTAN


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("asin"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "asin",//formula name
                    formula = "asin(x)",//formula
                    rearrangedFormula = "asin(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },



                    "asin(x)"//formula formula use
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }




            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("acos"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "acos",//formula name
                    formula = "acos(x)",//formula
                    rearrangedFormula = "acos(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "acos(x)"//formula formula use
                    
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("atan"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "atan",//formula name
                    formula = "atan(x)",//formula
                    rearrangedFormula = "atan(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "atan(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }

            //-----------------ARC SINCOSTAN H
            //-----------------ARC SINCOSTAN H
            //-----------------ARC SINCOSTAN H
            //-----------------ARC SINCOSTAN H

            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("asinh"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "asinh",//formula name
                    formula = "asinh(x)",//formula
                    rearrangedFormula = "asinh(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "asinh(x)"//formula formula use
                    
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }




            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("acosh"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "acosh",//formula name
                    formula = "acosh(x)",//formula
                    rearrangedFormula = "acosh(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "acosh(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("atanh"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "atanh",//formula name
                    formula = "atanh(x)",//formula
                    rearrangedFormula = "atanh(x)",//formula rearranged
                    description = "",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "atanh(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            //====================Log/ln
            //====================Log/ln
            //====================Log/ln
            //====================Log/ln

            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("log"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "log",//formula name
                    formula = "log(x)",//formula
                    rearrangedFormula = "log(x)",//formula rearranged
                description = "Log base 10",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "log(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("ln"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "ln",//formula name
                formula = "ln(x)",//formula
                    rearrangedFormula = "ln(x)",//formula rearranged
                    description = "Log base e",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "ln(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("abs"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "abs",//formula name
                    formula = "abs(x)",//formula
                    rearrangedFormula = "abs(x)",//formula rearranged
                    description = "absolute value of x",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "abs(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("ceiling"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "ceiling",//formula name
                    formula = "ceiling(x)",//formula
                    rearrangedFormula = "ceiling(x)",//formula rearranged
                    description = "returns the highest integer",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "ceiling(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }


            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("floor"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "clamp",//formula name
                    formula = "clamp(value,min,max)",//formula
                    rearrangedFormula = "clamp(value, min, max)",//formula rearranged
                description = "Set the value only between min or max",//formula description
                    variableList_Bind = new ObservableCollection<VariableData> {
                        new VariableData(
                        //"x",//variable
                        "value",//variable name
                        "value",//variable value
                        "an expression"),
                        new VariableData(
                        //"x",//variable
                        "min",//variable name
                        "min",//variable value
                        "minimum value"),
                        new VariableData(
                        //"x",//variable
                        "max",//variable name
                        "max",//variable value
                        "maximum value"),

                    },
                },
                    
                    
                    
                    "clamp(value,min,max)"//formula formula use
                    
                   
                    //variable description
                    )
                );
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }

            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("floor"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "floor",//formula name
                    formula = "floor(x)",//formula
                    rearrangedFormula = "floor(x)",//formula rearranged
                    description = "returns the lowest integer",//formula description
                variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x")},
                },
                    
                    
                    
                    "floor(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("fact!"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "fact!",//formula name
                    formula = "fact!(x)",//formula
                    rearrangedFormula = "fact!(x)",//formula rearranged
                    description = "returns the factorial of x; x is turned into an integer",//formula description
                variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "x is an integer")},
                },
                    
                    
                    
                    "fact!(x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }



            if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey("sum"))
            {
                FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Add(
                new CustomFunction(new Formula()
                {
                    formulaName = "sum",//formula name
                    formula = "sum(i,n,x)",//formula
                    rearrangedFormula = "sum(i,n,x)",//formula rearranged
                    description = "Summation; Slow performance if upper limit n is set too high; Will loop;",//formula description
                    variableList_Bind =
                    new ObservableCollection<VariableData> { new VariableData(
                        //"x",//variable
                        "i",//variable name
                        "i",//variable value
                        "index of summation (smaller or equal to n)"),
                        new VariableData(
                        //"x",//variable
                        "n",//variable name
                        "n",//variable value
                        "upper limit of summation"),
                        new VariableData(
                        //"x",//variable
                        "x",//variable name
                        "x",//variable value
                        "expression (can include \"i\")")
                    },
                },
                    
                    
                    
                    "sum(i,n,x)"//formula formula use
                    
                    //variable description
                    ));
                int index = FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind.Count - 1;
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index].formulaObj.formulaName,
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfDefaultTab].customFunctionCollection_Bind[index]);
            }





        }
    }
}
