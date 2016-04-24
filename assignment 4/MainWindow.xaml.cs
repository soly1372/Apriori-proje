using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace assignment_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //public List<string> subset(List<string> l , int n ) 
        //{

        //}
        public Dictionary<string,int> subset(Dictionary<string , int> x, int n)
        {
            List<List<string>> sub = new List<List<string>>();
            List<List<string>> answer = new List<List<string>>();
            foreach (var a in x)
            {
                string s = a.Key;
                string[] ss = s.Split(',');
                List<string> temp = new List<string>();
                foreach (var v in ss)
                {
                    temp.Add(v);
                   
                }
                sub.Add(temp);
            }
            for (int i = 0; i < sub.Count; i++)
            {
                List<string> C = new List<string>();
                foreach (var t in sub[i])
                {
                    C.Add(t);
                }
                List<string> E = new List<string>();
                foreach (var p in C)
                {
                    E.Add(p);
                }
                for (int j = i + 1; j < sub.Count; j++)
                {
                    
                    
                    
                    foreach (var y in sub[j])
                    {
                        if (E.Contains(y) == false)
                        {
                            E.Add(y);
                        }
                    }
                    if (E.Count == n)
                    {
                        E.Sort();
                        answer.Add(E);
                        E = new List<string>();
                        foreach (var p in C)
                        {
                            E.Add(p);
                        }
                    }
                }
                
            }
            Dictionary<string, int> ans = new Dictionary<string, int>();
            for (int i = 0; i < answer.Count; i++)
            {
                string s = "";
                int j;
                for (j = 0; j < answer[i].Count - 1; j++)
                {
                    s += answer[i][j]  +',';
                }
                s += answer[i][j];
                if(ans.ContainsKey(s)==false)
                    ans.Add(s, 0);
            }
            return ans;
        }
        int minsup;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string way = ""; 
            var a = new OpenFileDialog();
            var result = a.ShowDialog();
            if (result == false) return;
            way = a.FileName;

            var reader = new StreamReader(File.OpenRead(way));
            reader.ReadLine();
            List<string> A = new List<string>();
            List<string> B = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                line = line.Replace(" ", "");
                var values = line.Split(',');
                
                A.Add(values[0]);
                B.Add(values[1]);
                              
            }
            
            List<string> C = new List<string>();
           string f = "";
            string t = "" ;
           
            for (int i = 0; i < B.Count; i++)
            {
                 f = B[i];
                var s =f.Split(';');
                for (int k = 0; k < s.Length; k++)
                {
                    t += s[k];
                }
                C.Add(t);
                t = "";                         
            }
            var sep = "";
            List<string> D = new List<string>(); 
            for (int i = 0; i < C.Count; i++)
            {
                sep += C[i]; 
            }
            for (int i = 0; i < sep.Length; i++)
            {
                D.Add(sep[i].ToString());
            }
            List<List<string>> L = new List<List<string>>();
            
            for (int i = 0; i < B.Count; i++)
            {
                List<string> w = new List<string>();
                string c = B[i];
               
                var  p = c.Split(';');
                for (int j = 0; j < p.Length; j++)
                {
                    w.Add(p[j]);
                }
                L.Add(w);
            }
            Dictionary<string, int> counts = D.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            counts.Remove(" ");
            List<string> E = new List<string>();
            
            foreach (var x in counts)
            {
                if (x.Value < minsup)
                {
                    E.Add(x.Key);
                }

            }
            foreach (var pp in E)
            {
                counts.Remove(pp);
            }
            int n = 2; 
            Dictionary<string, int> F = new Dictionary<string, int>();
            Dictionary<string, int> Last = counts;
            while (true)
            {
                
                Dictionary<string, int> temp = new Dictionary<string, int>();
                F = subset(counts, n);
                bool flag = true;
                int counter = 0;

                foreach (var x in F)
                {
                    string s = x.Key;
                    counter = 0;
                    string[] ss = s.Split(',');
                    flag =true;
                    for (int j = 0; j <L.Count ; j++)
                    {
                        flag = true;
                        for (int i = 0; i < ss.Length; i++)
                        {
                            if (!L[j].Contains(ss[i]))
                            {
                                flag = false;
                                break;
                            }
                          
                        }
                        if (flag == true)
                        {
                            counter++;
                            flag = true;
                        }
                        

                    }
                   // x.Value = counter;
                   //F[x.Key] = counter;
                    if (counter >= minsup)
                    {
                        temp.Add(x.Key, counter);
                        counter = 0;
                    }
                    
                }
                if (temp.Count == 0)
                {
                    break;
                }
                
                F = temp;
                Last = F;
                
                n++;
                
            }
            g1.ItemsSource = Last;
            
            
           
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private  void Button_Click_2(object sender, RoutedEventArgs e)
        {
            minsup = int.Parse(text1.Text); 
        }
    }
}
