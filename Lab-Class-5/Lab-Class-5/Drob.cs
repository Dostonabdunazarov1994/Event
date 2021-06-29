using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Class_5
{
    class Drob
    {
        public bool Changable { get; set; }
        public bool Simple { get { return Math.Abs(P) < Q; } }
        public delegate void ChangeHandler(Drob sender, EventArgs e);
        public delegate void ZeroHandler(Drob sender, DrobArgs e);
        public delegate void ChangingHandler(Drob sender, DrobArgs e);
        string _name;
        int _p;
        int _q;
        public event ChangeHandler Onchange;
        public event ZeroHandler Onzero;
        public event ChangingHandler OnChanging;
        public string name { get { return _name; } }
        public int P { get { return _p; } set { SetDrob(value, Q); } }
        public int Q { get { return _q; } set { SetDrob(P, value); } }
        public double Value
        {
            get { return (double)P / Q; }
            set
            {
                if (Math.Abs(value) > 1e9) return;
                int qq = 1;
                while(Math.Truncate(value) != value)
                {
                    value *= 10;
                    qq *= 10;
                    if (qq >= 1e8) break;
                }
                SetDrob((int)value, qq);
            }
        }
        
        public Drob(string name, ChangeHandler ch, ZeroHandler z, ChangingHandler changing)
        {
            _name = name;
            Onchange = ch;
            Onzero = z;
            Changable = true;
            OnChanging = changing;
            SetDrob(1, 1);
        }
        public Drob() { P = 1; Q = 1; Value = 1; }
        public void SetDrob(int newP, int newQ)
        {
            if (newP != P || newQ != Q)
            {
                int pp = P, qq = Q;
                if (newQ != 0)
                {
                    Normalization(ref newP, ref newQ);
                    _p = newP; _q = newQ;
                }
                else
                {
                    _p = newP; _q = 1;
                    if (Onzero != null)
                        Onzero(this, new DrobArgs(pp, qq));
                }
                if (OnChanging != null)
                    OnChanging(this, new DrobArgs(pp, qq));
                if (Onchange != null)
                    Onchange(this, new EventArgs());
               
            }
        }
        public override string ToString()
        {
            return string.Format("Дробь {0,5} = {1,5} / {2,-5} = {3,-8:F3}", name, P, Q, Value);
        }

        public void Normalization(ref int newP, ref int newQ)
        {

            if (newQ < 0)
            {
                newQ *= -1;
                newP *= -1;
            }
            int p = Math.Abs(newP);
            int q = Math.Abs(newQ);
            if (p > 1)
            {
                newP /= Nod(p, q);
                newQ /= Nod(p, q);
            }
        }
        static int Nod(int x, int y)
        {
            while (x != y)
            {
                if (x > y)
                    x -= y;
                else
                    y -= x;
            }
            return x;
        }
        public void Assign(Drob a)
        {
            if(Changable)
            SetDrob(a.P, a.Q);
        }
        public int Compare(Drob d)
        {
            if (Value == d.Value)
                return 0;
            if (Value > d.Value)
                return 1;
            return -1;
        }
        public void Swap() { if(Changable) SetDrob(Q, P); }
        public void Add(Drob a) { if (Changable) SetDrob(P * a.Q + a.P * Q, Q * a.Q); }
        public void Sub(Drob a) { if (Changable) SetDrob(P * a.Q - a.P * Q, Q * a.Q); }
        public void Mult(Drob a) { if (Changable) SetDrob(P * a.P, Q * a.Q); }
        public void Div(Drob a) { if (Changable) if (a.P != 0) SetDrob(P * a.Q, Q * a.P); }
    }
}
