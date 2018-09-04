﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class Extend
    {   
       public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
       {
            return new ObservableCollection<T>(_LinqResult);
       }   
    }
}
