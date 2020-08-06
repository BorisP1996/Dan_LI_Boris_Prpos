using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static SemaphoreSlim sem = new SemaphoreSlim(3);

        static int count = 5;
        static int count1 = 10;
        static int N = 3;

        static int c = 0;
        static void Main(string[] args)
        {
            //int i = 7;
            //do
            //{
            //    i++;
            //    if (i%2==0)
            //    {
            //        continue;
            //    }
            //    Console.WriteLine(i);
            //} while (i%4==0);

            //string test = "Test iz ththreading-a";
            //for (int i = 0; i < test.Length; i++)
            //{
            //    Console.Write(test[i] == 'e' ? 'x' : test[i]);
            //}

            //int a = 0;
            //for (int i = 2; i < 6; i+=2)
            //{
            //    a += i;
            //    Thread t = new Thread(myFun);
            //    t.Start(a);
            //}
            //Console.WriteLine("main thread running");

            //Thread t = new Thread(myFun1);
            //Thread t2 = t;
            //t.Name = "Thread1";
            //t.Start();
            //Console.WriteLine("Main thread running");

            //Thread t = new Thread(myFun2);
            //t.IsBackground = true;
            //t.Start();
            //Console.WriteLine("done...");

            //for (int i = 5; i >=1; i--)
            //{
            //    new Thread(() => new Thread(Work1).Start()).Start();
            //}

            //for (int i = 1; i <=5; i++)
            //{
            //    new Thread(Enter).Start(i);
            //}

            //int num = 109, s = 0, r;
            //while (num!=0)
            //{
            //    r = num % 10;
            //    num = num / 10;
            //    s = s + r;
            //}
            //Console.WriteLine(s);

            //Thread thread = new Thread(new ThreadStart(Func1));
            //thread.Start();
            //Console.WriteLine("Thread is started");

            //try
            //{
            //    int a = 2;
            //    method2(a);
            //    Console.WriteLine("a={0}",a);

            //    method3(ref a);
            //    Console.WriteLine("a={0}",a);
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("error");
            //}

            //for (int i = 0; i < count1; i++)
            //{
            //    new Thread(Test).Start(i);
            //}
            //Console.WriteLine("Done");

            //Thread t1 = new Thread(new ThreadStart(print));
            //Thread t2 = new Thread(new ThreadStart(print));
            //Thread t3 = new Thread(new ThreadStart(print));

            //t1.Start();
            //t2.Start();
            //t2.Join();
            //t1.Join();
            //t3.Start();
            //Console.WriteLine(zad11);
            Thread t1 = new Thread(new ThreadStart(Work11));
            Thread t2 = new Thread(new ThreadStart(Work2));
            t1.Start();
            t2.Start();
            Console.ReadLine();
        }
        static object ob1 = new object();
        static object ob2 = new object();
        public static void Work11()
        {
            lock (ob1)
            {
                Console.WriteLine("50 done");
                Thread.Sleep(500);
                lock (ob2)
                {
                    Console.WriteLine("100 done");
                }
            }
        }
        public static void Work2()
        {
            lock (ob2)
            {
                Console.WriteLine("50 done");
                Thread.Sleep(500);
                lock (ob1)
                {
                    Console.WriteLine("100 done");
                }
            }
        }
        static object o = new object();
        static int zad11 = 0;
        private static void print()
        {
            lock (o)
            {
                zad11++;
                zad11 -= 1;
            }
        }
        static void Test(object i)
        {
            try
            {
                Thread.Sleep(1000);
            }
            finally
            {

                lock (new object())
                {
                    N += 2;
                    Console.WriteLine(N);
                }
            }
        }
        protected static void method2(int broj)
        {
            broj += 10;
        }
        protected static void method3(ref int broj)
        {
            broj += 10;
        }
        static void Func1()
        {
            Thread thread = new Thread(new ThreadStart(Func1));
            return;
            thread.Start();
            Console.WriteLine("Thread func!");
        }
        static void Enter(object obj)
        {
            Console.WriteLine(obj + "wants to enter");
            sem.Wait();
            Console.WriteLine(obj + "is in");
            Thread.Sleep(1000 * (int)obj);
            Console.WriteLine(obj+ "is leaving");
            // Console.ReadLine();
            sem.Release();
        }
        //static void myFun(object o)
        //{
        //    var res = (int)o % 2;
        //    Console.WriteLine(res);
        //}
        static void myFun1()
        {
            count += 5;
            Thread.Sleep(3000);
            Console.WriteLine("Thread {0} completed, count {1}",Thread.CurrentThread.Name,count);
        }
        static void myFun2()
        {
            Thread.CurrentThread.IsBackground = false;
            
            Console.WriteLine("1");
            Thread.Sleep(2000);
            Console.WriteLine("2");
        }
        static void Work(int x)
        {
            if (Thread.CurrentThread.ThreadState==ThreadState.WaitSleepJoin)
            {
                Console.WriteLine(x);
            }
        }
        static void Work1()
        {
            Console.WriteLine(c++);
        }
    }
}
