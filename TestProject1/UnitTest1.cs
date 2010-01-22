﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    enum Places
    {
        p1 = 0, p2 = 1, p3 = 2, p4 = 3
    }
    enum Transitions
    {
        t1 = 0, t2 = 1, t3 = 2
    }

    [TestClass]
    public class TestClass2
    {

        [TestMethod]
        public void TestTest1()
        {
            var p = CreatePNTwoInOneOut();
            AssertMarkings(p, new Dictionary<int, int>{ 
                { 0, 1 }, 
                { 1, 1 },
                { 2, 0 } });
            p.Fire();
            AssertMarkings(p, new Dictionary<int, int>{ 
                { 0, 0 }, 
                { 1, 0 },
                { 2, 1 } });

        }

        public static MatrixPetriNet CreatePNTwoInOneOut()
        {
            var p = new MatrixPetriNet(
                "p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"},
                    {2, "p2"}
                },
                new Dictionary<int, int> { { 0, 1 }, { 1, 1 }, { 2, 0 } },
                new Dictionary<int, string> { { 0, "t0" } },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0),new InArc(1)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(2)}}
                }
              );
            return p;
        }

        public void AssertMarkings<T1, T2>(MatrixPetriNet p, Dictionary<T1, T2> markingsExpected)
        {
            foreach (var marking in markingsExpected)
            {
                Assert.AreEqual(marking.Value, p.GetMarking(Convert.ToInt32(marking.Key)));
            }
        }


    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"},
                    {2, "p2"}
                },
                new Dictionary<int, int> { { 0, 1 }, { 1, 1 }, { 2, 0 } },
                new Dictionary<int, string> { { 0, "t0" } },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0),new InArc(1)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(2)}}
                });
            Assert.AreEqual(1, p.GetMarking(1));
        }

        [TestMethod]
        public void Test2()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"}
                },
                new Dictionary<int, int> { { 0, 0 }, { 1, 0 }},
                new Dictionary<int, string> { { 0, "t0" } },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(1)}}
                });

            p.SetMarking(0, 1);
            Assert.AreEqual(true, p.IsEnabled(0));
        }
        
        [TestMethod]
        public void Test3()
        {
            var p = TestClass2.CreatePNTwoInOneOut();

            Assert.AreEqual(true, p.IsEnabled(0));
            p.SetMarking(0, 0);
            Assert.AreEqual(false, p.IsEnabled(0));
        }
        
        [TestMethod]
        public void Test4()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"}
                },
                new Dictionary<int, int> { { 0, 1 }, {1,0} },
                new Dictionary<int, string> { { 0, "t0" } },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(1)}}
                });

            Assert.AreEqual(1, p.GetMarking(0));
            Assert.AreEqual(0, p.GetMarking(1));
            Assert.AreEqual(true, p.IsEnabled(0));
            p.Fire();
            Assert.AreEqual(0, p.GetMarking(0));
            Assert.AreEqual(1, p.GetMarking(1));
        }

        [TestMethod]
        public void Test5()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"},
                    {2, "p2"}
                },
                new Dictionary<int, int> { { 0, 1 }, { 1, 0 }, { 2, 0 } },
                new Dictionary<int, string> { { 0, "t0" } },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0),new InArc(2)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(1)}}
                });

            Assert.AreEqual(false, p.IsEnabled(0));
            p.SetMarking(2, 1);
            Assert.AreEqual(true, p.IsEnabled(0));
            p.Fire();
            Assert.AreEqual(0, p.GetMarking(0));
            Assert.AreEqual(1, p.GetMarking(1));
            Assert.AreEqual(0, p.GetMarking(2));
        }

        [TestMethod]
        public void Test6()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {(int)Places.p1, "p1"},
                    {(int)Places.p2, "p2"},
                    {(int)Places.p3, "p3"},
                    {(int)Places.p4, "p4"}
                },
                new Dictionary<int, int> 
                    { 
                        { (int)Places.p1, 0 }, 
                        { (int)Places.p2, 0 }, 
                        { (int)Places.p3, 0 }, 
                        { (int)Places.p4, 0 } 
                    },
                new Dictionary<int, string> 
                    { 
                        { (int)Transitions.t1, "t1" },
                        { (int)Transitions.t2, "t2" },
                        { (int)Transitions.t3, "t3" }
                    },
                new Dictionary<int, List<InArc>>(){
                    {(int)Transitions.t1, new List<InArc>(){new InArc((int)Places.p1)}},
                    {(int)Transitions.t2, new List<InArc>(){new InArc((int)Places.p2)}},
                    {(int)Transitions.t3, new List<InArc>(){new InArc((int)Places.p4)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {(int)Transitions.t1, new List<OutArc>(){new OutArc((int)Places.p2)}},
                    {(int)Transitions.t2, new List<OutArc>(){new OutArc((int)Places.p3)}},
                    {(int)Transitions.t3, new List<OutArc>(){new OutArc((int)Places.p2)}}
                });

            /*
             * This model is a petri net in this shape
             * 
             * P1 --> T1 --> P2 --> T2 --> P3
             *                ^
             *                |
             *                T3
             *                ^
             *                |
             *                P4
             * */
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 0 },
                { Places.p3, 0 },
                { Places.p4, 0 } });

            p.SetMarking((int)Places.p1, 1);
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 1 }, 
                { Places.p2, 0 },
                { Places.p3, 0 },
                { Places.p4, 0 } });

            p.Fire();
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 1 },
                { Places.p3, 0 },
                { Places.p4, 0 } });

            p.Fire();
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 0 },
                { Places.p3, 1 },
                { Places.p4, 0 } });

            p.SetMarking((int)Places.p4, 1);
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 0 },
                { Places.p3, 1 },
                { Places.p4, 1 } });

            p.Fire();
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 1 },
                { Places.p3, 1 },
                { Places.p4, 0 } });

            p.Fire();
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 0 },
                { Places.p3, 2 },
                { Places.p4, 0 } });
        }

        [TestMethod]
        public void Test7() // a bifurcating transition
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {(int)Places.p1, "p1"},
                    {(int)Places.p2, "p2"},
                    {(int)Places.p3, "p3"}
                },
                new Dictionary<int, int> 
                    { 
                        { (int)Places.p1, 0 }, 
                        { (int)Places.p2, 0 }, 
                        { (int)Places.p3, 0 } 
                    },
                new Dictionary<int, string> 
                    { 
                        { (int)Transitions.t1, "t1" }
                    },
                new Dictionary<int, List<InArc>>(){
                    {(int)Transitions.t1, new List<InArc>(){new InArc((int)Places.p1)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {(int)Transitions.t1, new List<OutArc>(){new OutArc((int)Places.p2),
                                                             new OutArc((int)Places.p3)}}
                });

            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 0 },
                { Places.p3, 0 } });

            p.SetMarking((int)Places.p1, 1);
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 1 }, 
                { Places.p2, 0 },
                { Places.p3, 0 } });

            p.Fire();
            AssertMarkings(p, new Dictionary<Places, int>{ 
                { Places.p1, 0 }, 
                { Places.p2, 1 },
                { Places.p3, 1 } });
        }

        [TestMethod]
        public void TestSelfTransition()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {(int)Places.p1, "p1"}
                },
                new Dictionary<int, int> 
                    { 
                        { (int)Places.p1, 0 } 
                    },
                new Dictionary<int, string> 
                    { 
                        { (int)Transitions.t1, "t1" }
                    },
                new Dictionary<int, List<InArc>>(){
                    {(int)Transitions.t1, new List<InArc>(){new InArc((int)Places.p1)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {(int)Transitions.t1, new List<OutArc>(){new OutArc((int)Places.p1)}}
                });
            p.SetMarking((int)Places.p1, 1);
            Assert.AreEqual(1, p.GetMarking((int)Places.p1));
            p.Fire();
            Assert.AreEqual(1, p.GetMarking((int)Places.p1));
        }

        [TestMethod]
        public void TestDoubleSelfTransition()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {(int)Places.p1, "p1"}
                },
                new Dictionary<int, int> 
                    { 
                        { (int)Places.p1, 1 } 
                    },
                new Dictionary<int, string> 
                    { 
                        { (int)Transitions.t1, "t1" },
                        { (int)Transitions.t2, "t2" }
                    },
                new Dictionary<int, List<InArc>>(){
                    {(int)Transitions.t1, new List<InArc>(){new InArc((int)Places.p1)}},
                    {(int)Transitions.t2, new List<InArc>(){new InArc((int)Places.p1)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {(int)Transitions.t1, new List<OutArc>(){new OutArc((int)Places.p1)}},
                    {(int)Transitions.t2, new List<OutArc>(){new OutArc((int)Places.p1)}}
                });
            Assert.AreEqual(1, p.GetMarking((int)Places.p1));
            p.Fire();
            Assert.AreEqual(1, p.GetMarking((int)Places.p1));
        }
  
        public void AssertMarkings<T1, T2>(MatrixPetriNet p, Dictionary<T1, T2> markingsExpected)
        {
            foreach (var marking in markingsExpected)
            {
                Assert.AreEqual(marking.Value, p.GetMarking(Convert.ToInt32(marking.Key)));
            }
        }

        [TestMethod]
        public void TestLoadPnmlFile()
        {
            var path = @"C:\shared.datastore\repository\personal\dev\prototypes\Automata\PetriNet\pnml.ex1.xml";
            var pnSeq = PnmlModelLoader.Load(path).ToList();
            Assert.IsNotNull(pnSeq);
            Assert.AreEqual(2, pnSeq.Count());
            Assert.AreEqual("n2", pnSeq.ElementAt(0).Id);
            Assert.AreEqual("n1", pnSeq.ElementAt(1).Id);
            var pn1 = pnSeq[0];
            Assert.AreEqual(2, pn1.Places.Count());
            Assert.AreEqual(1, pn1.Transitions.Count());
            Assert.AreEqual(1, pn1.PlaceOutArcs.Count);
            Assert.AreEqual(1, pn1.InArcs.Count);
            Assert.AreEqual(1, pn1.OutArcs.Count);
            Assert.AreEqual(1, pn1.Markings.Count);
            pn1.Fire();
        }

        [TestMethod]
        public void TestTransitionFunctionExecution()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"}
                },
                new Dictionary<int, int> 
                    { 
                        { 0, 2 } ,
                        { 1, 0 } 
                    },
                new Dictionary<int, string> 
                    { 
                        { 0, "t0" }
                    },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(1)}}
                });
            Assert.AreEqual(2, p.GetMarking(0));
            var someLocal = 0;
            p.SetMarking(0, 1);
            p.RegisterFunction(0, (t)=>someLocal+=1);
            p.Fire();
            Assert.AreEqual(1,someLocal); 
        }


        [TestMethod]
        public void TestFireConflictingTransitions()
        {
            var p = new MatrixPetriNet("p",
                new Dictionary<int, string> {
                    {0, "p0"},
                    {1, "p1"},
                    {2, "p2"}
                },
                new Dictionary<int, int> 
                    { 
                        { 0, 1 } ,
                        { 2, 0 } ,
                        { 1, 0 } 
                    },
                new Dictionary<int, string> 
                    { 
                        { 0, "t1" }
                    },
                new Dictionary<int, List<InArc>>(){
                    {0, new List<InArc>(){new InArc(0),new InArc(2)}}
                },
                new Dictionary<int, List<OutArc>>(){
                    {0, new List<OutArc>(){new OutArc(1)}}
                });

            Assert.AreEqual(false, p.IsEnabled(0));
            p.SetMarking(2, 1);
            Assert.AreEqual(true, p.IsEnabled(0));
        }
    }
}