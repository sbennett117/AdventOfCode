using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day07
    {
        public string TaskList()
        {
            string[] lines = System.IO.File.ReadAllLines(@"input/Day07.txt");
            SortedDictionary<char, Task> tasks = new SortedDictionary<char, Task>();
            foreach(string line in lines)
            {
                string[] splitLine = line.Split(' ');
                char key = splitLine[7][0];
                char d = splitLine[1][0];
                if (!tasks.ContainsKey(key)) tasks.Add(key, new Task(key));
                if (!tasks.ContainsKey(d)) tasks.Add(d, new Task(d));
                tasks[key].AddDependency(d);
            }

            string taskList = "" + NextTask(tasks, "");
            tasks.Remove(taskList[0]);
            while (taskList[taskList.Length - 1] != '!')
            {
                taskList += NextTask(tasks, taskList);
                tasks.Remove(taskList[taskList.Length-1]);
            }

            return taskList;
        }

        public int TimeToComplete()
        {
            string[] lines = System.IO.File.ReadAllLines(@"input/Day07.txt");
            SortedDictionary<char, Task> tasks = new SortedDictionary<char, Task>();
            foreach(string line in lines)
            {
                string[] splitLine = line.Split(' ');
                char key = splitLine[7][0];
                char d = splitLine[1][0];
                if (!tasks.ContainsKey(key)) tasks.Add(key, new Task(key));
                if (!tasks.ContainsKey(d)) tasks.Add(d, new Task(d));
                tasks[key].AddDependency(d);
            }

            int ttc = 0;
            string completed = "";
            List<Task> workers = new List<Task>();

            while (tasks.Count > 0 || workers.Count > 0)
            {
                while (workers.Count < 6)
                {
                    char task = NextTask(tasks, completed);
                    if (task == '!') break;
                    workers.Add(tasks[task]);
                    tasks.Remove(task);
                }

                int shortestTTC = int.MaxValue;
                foreach (Task t in workers)
                {
                    shortestTTC = Math.Min(shortestTTC, t.TTC);
                }

                ttc += shortestTTC;
                for (int i = 0; i < workers.Count;)
                {
                    workers[i].TTC -= shortestTTC;
                    if (workers[i].TTC == 0)
                    {
                        completed += workers[i].ID;
                        workers.RemoveAt(i);
                    }
                    else i++;
                }
            }

            return ttc;
        }

        char NextTask(SortedDictionary<char, Task> tasks, string completed) {
            foreach(char k in tasks.Keys)
            {
                foreach (char c in completed) tasks[k].RemoveDependency(c);
                if (tasks[k].CanExecute()) return k;
            }

            return '!';
        }
    }

    class Task : IComparable
    {
        public char ID { get; set; }
        readonly ISet<char> dependencies;
        public int TTC { get; set; }

        public Task(char id)
        {
            ID = id;
            dependencies = new HashSet<char>();
            TTC = 61 + (id - 'A');  // A = 61 + 0; B = 61 + 1; etc;
        }

        public bool AddDependency(char d)
        {
            return dependencies.Add(d);
        }

        public bool RemoveDependency(char d)
        {
            return dependencies.Remove(d);
        }

        public bool CanExecute() {
            return dependencies.Count == 0;
        }

        public int CompareTo(object o)
        {
            if (o == null) return 1;

            Task other = o as Task;

            return ID.CompareTo(other.ID);
        }

        public override string ToString()
        {
            string s = ID + ": [";
            foreach(char d in dependencies)
            {
                s += d;
            }
            return s + ']';
        }
    }
}
