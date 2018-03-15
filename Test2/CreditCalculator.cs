using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    class CreditCalculator
    {
        public struct credit
        {
            public double debtPayment;
            public double creditProcent;
            public double debtRemainder;
            public double prepayment;
        }
        private credit[] datas;
        private double mounthPayment;
        private double creditSumm;
        private const double rate = 0.12;
        private int mounthsCount;
        private int topIndex;

        private void mounthPaymentCalculation()
        {
            double p = rate / 12;
            mounthPayment = creditSumm * (p + (p / (power(1 + p, mounthsCount) - 1)));
        }

        private double power(double value, int pow)
        {
            double result = 1;
            for (int i = 0; i < pow; i++)
                result = result * value;
            return result;
        }

        private void remainderCalculate(int index)
        {
            datas[index].debtRemainder = (index > 0) ? datas[index-1].debtRemainder - datas[index].debtPayment - datas[index-1].prepayment: creditSumm - (mounthPayment - datas[index].creditProcent);
        }

        private void procentCalculate(int index)
        {
            datas[index].creditProcent = (index > 0) ? datas[index - 1].debtRemainder * rate / 12 : creditSumm * rate / 12;
        }

        private void debtPaymentCalculate(int index)
        {
            datas[index].debtPayment = mounthPayment - datas[index].creditProcent;
        }

        private void initializeOneMounth(int index)
        {
            procentCalculate(index);            
            debtPaymentCalculate(index);
            remainderCalculate(index);
            
        }

        private void createTable()
        {
            datas = new credit[mounthsCount];
            for (int i = 0; i < mounthsCount; i++)
            {
                initializeOneMounth(i);
                datas[i].prepayment = 0;
                if (datas[i].debtRemainder <= 0)
                    break;
            }
        }

        public CreditCalculator(double creditSumm, int mounthsCount)
        {
            this.creditSumm = creditSumm;
            this.mounthsCount = mounthsCount;
            topIndex = mounthsCount;
            mounthPaymentCalculation();
            createTable();
        }

        public double getMountPayment()
        {
            return mounthPayment;
        }

        public double getCreditSumm()
        {
            return creditSumm;
        }

        public int getMounthsCount()
        {
            return mounthsCount;
        }

        public int getTopIndex()
        {
            return topIndex;
        }

        public credit[] getDatas()
        {
            credit[] copy = new credit[mounthsCount];
            for (int i = 0; i < mounthsCount; i++)
                copy[i] = datas[i];
            return copy;
        }

        public void changeDatas(int index, double prepayment)
        {
            datas[index].prepayment = prepayment;
            for(int i = index+1; i < mounthsCount; i++)
            {
                initializeOneMounth(i);
                if (datas[i].debtRemainder <= 0)
                {
                    topIndex = i + 1;
                    return;
                }
            }
            topIndex = mounthsCount;
        }
    }
}
