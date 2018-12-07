using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day07
    {
        public string TaskList()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Day07.txt");
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

            string taskList = "";
            while (NextTask(tasks, taskList) != '!')
            {
                taskList += NextTask(tasks, taskList);
                tasks.Remove(taskList[taskList.Length-1]);
            }

            return taskList;
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
        ISet<char> dependencies;

        public Task(char id)
        {
            this.ID = id;
            this.dependencies = new HashSet<char>();
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
