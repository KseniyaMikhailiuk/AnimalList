using System;

namespace AnimalList
{
    class Zoo<T> where T: Animal, new()
    {
        T[] animals = new T[] { };

        public ActionTypes GetAction()
        {
            Console.WriteLine(String.Format("Select action. {0} - Add. {1} - Remove. {2} - Get Name By Index. " +
                                            "{3} - Get Index By Name. {4} - Print. {5} - Clear. {6} - Exit.",
                   (int)ActionTypes.Add,
                   (int)ActionTypes.Remove,
                   (int)ActionTypes.GetNameByIndex,
                   (int)ActionTypes.GetIndexByName,
                   (int)ActionTypes.Print,
                   (int)ActionTypes.Clear,
                   (int)ActionTypes.Exit));

            int pressedKey = -1;
            bool isKeyDefined = false;

            while (!isKeyDefined)
            {
                int.TryParse(Console.ReadLine(), out pressedKey);
                isKeyDefined = Enum.IsDefined(typeof(ActionTypes), pressedKey);

                if (!isKeyDefined)
                {
                    Console.WriteLine("Something went wrong. Try again.");
                }
            }

            return (ActionTypes)pressedKey;
        }

        public void Start()
        {
            bool isExitKeyPressed = false;
            while (!isExitKeyPressed)
            {
                var action = GetAction();

                isExitKeyPressed = action == ActionTypes.Exit;
                if (isExitKeyPressed)
                {
                    return;
                }
                ExecuteAction(action);
            }
        }

        public void ExecuteAction (ActionTypes action)
        {
            switch(action)
            {
                case (ActionTypes.Add):
                    Console.WriteLine("Name:");
                    string nameToAdd = Console.ReadLine();
                    Add(nameToAdd);
                    break;
                case (ActionTypes.Remove):
                    Console.WriteLine("Name:");
                    string nameToRemove = Console.ReadLine();
                    Remove(nameToRemove);
                    break;
                case (ActionTypes.GetIndexByName):
                    Console.WriteLine("Name:");
                    string nameToFind = Console.ReadLine();
                    Console.WriteLine(GetIndexByName(nameToFind));
                    break;
                case (ActionTypes.GetNameByIndex):
                    Console.WriteLine("Index:");
                    if (Int32.TryParse(Console.ReadLine(), out int index))
                    {
                        if ((index - 1 >= 0) && (index - 1 < animals.Length))
                        {
                            Console.WriteLine(animals[index - 1].Name);
                        }
                        else
                        {
                            Console.WriteLine("Wrong index");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong index");
                    }
                    break;
                case (ActionTypes.Print):
                    Print();
                    break;
                case (ActionTypes.Clear):
                    Clear();
                    break;
            }
        }

        public void Add(string name)
        {
            var animal = new T();
            animal.Name = name;
            Array.Resize(ref animals, animals.Length + 1);
            animals[animals.Length - 1] = animal;
        }

        public T this[int index]
        {
            get
            {
                return animals[index - 1];
            }
        }
        
        public string GetIndexByName(string name)
        {
            int i = 0;
            bool isNameFound = false;
            while ((i < animals.Length) && (!isNameFound))
            {
                if (animals[i].Name == name)
                {
                    isNameFound = true;
                }
                i++;
            }
            if (!isNameFound)
            {
                string s = "There is no such name.";
                return s;
            }
            else
            {
                return Convert.ToString(i);
            }
        }

        public void Remove(string name)
        {
            if (Int32.TryParse(GetIndexByName(name), out int index))
            {
                index--;
                var temp = animals[index];
                animals[index] = animals[animals.Length - 1];
                animals[animals.Length - 1] = temp;
                Array.Resize(ref animals, animals.Length - 1);
            }
            else
            {
                Console.WriteLine("There is no such name.");
            }
        }

        public void Print()
        {
            if (animals.Length != 0)
            {
                foreach (T animal in animals)
                {
                    Console.WriteLine(animal.Name);
                }
            }
            else
            {
                Console.WriteLine("List is empty");
            }
        }

        public void Clear()
        {
            Array.Resize(ref animals, 0);
        }
    }
}
