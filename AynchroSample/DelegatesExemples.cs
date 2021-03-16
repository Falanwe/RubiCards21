using System;
using System.Collections.Generic;
using System.Text;

namespace AynchroSample
{
    public class DelegatesExemples
    {
        //public delegate void MyVoidDelegate();
        //public delegate double MyComplicatedDelegate(int arg1, int arg2, double arg3);

        private Action _someDelegate;

        public string Prefix { get; set; }
        public string Suffix { get; set; }



        public Func<string> DecorateStringFactory(Func<string> factory) => () => $"{Prefix} {factory()} {Suffix}";

        private class MyPseudoLambda
        {
            public DelegatesExemples Parent { get; set; }
            public Func<string> Factory { get; set; }

            public string Invoke() => $"{Parent.Prefix} {Factory()} {Parent.Suffix}";
        }
        public Func<string> DecorateStringFactoryWithoutLambda(Func<string> factory)
        {
            var pseudoLambda = new MyPseudoLambda { Factory = factory, Parent = this };
            return pseudoLambda.Invoke;
        }

        private void PrintHello() => Console.WriteLine("Hello");
        public void PrintHelloTwice() => ExecuteMyDelegateTwice(PrintHello);

        private double CalculateSomething(int arg1, int arg2, double arg3) => Math.Pow(arg3 / arg2, arg1);

        public void ExecuteMyDelegateTwice(Action myVoidDelegate)
        {
            if (myVoidDelegate != null)
            {
                myVoidDelegate();
                myVoidDelegate();
            }
        }

        public double NonSenseCalculation() => DoComplicatedStuffOtherwise(CalculateSomething)(2, 3, Math.PI);

        public double DoComplicatedStuff(Func<int, int, double, double> myComplicatedDelegate, int arg1, int arg2, double arg3)
        {
            var temp = myComplicatedDelegate(arg1, arg2, arg3);
            return myComplicatedDelegate(arg2, arg1, temp);
        }

        public Func<int, int, double, double> DoComplicatedStuffOtherwise(Func<int, int, double, double> myComplicatedDelegate)
        {
            return (arg1, arg2, arg3) => myComplicatedDelegate(arg2, arg1, myComplicatedDelegate(arg1, arg2, arg3));
        }

        public Func<int, int> SetSecondParameterAsFour(Func<int, int, int> someFunc)
        {
            //int result(int i)
            //{
            //    return someFunc(i, 4);
            //}

            //return result;

            return (int i) => (int)(someFunc(i, 4));
        }
    }
}
