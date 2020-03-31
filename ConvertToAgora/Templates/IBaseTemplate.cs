using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Templates
{
    interface IBaseTemplate
    {
        IEnumerable<object> GetFields();

        string ToString();

        void ValidateLine(string line);
    }
}
