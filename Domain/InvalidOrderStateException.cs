using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class InvalidOrderStateException: Exception
    {
        //The InvalidOrderStateException constructor
        public InvalidOrderStateException(string msg) : base(msg)
        {

        }

    }
}
