using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeanCounter.BusinessLogic
{
    public class Basket
    {

        public ofxFile ofxFile;
        public BankAccount BankAccount;

        public Basket(ofxFile ofxfile, BankAccount bankAccount)
        {
            ofxFile = ofxfile;
            BankAccount = bankAccount;
        }


    }
}
