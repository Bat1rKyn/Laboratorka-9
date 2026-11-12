using System;

namespace MoneyWPFApp
{
    public class Money
    {
        private uint _rubles;
        private byte _kopeks;

        // Конструкторы
        public Money()
        {
            _rubles = 0;
            _kopeks = 0;
        }

        public Money(uint rubles, byte kopeks)
        {
            // Нормализуем значения: переводим лишние копейки в рубли
            NormalizeValues(ref rubles, ref kopeks);
            _rubles = rubles;
            _kopeks = kopeks;
        }

        public Money(Money copyMoney)
        {
            _rubles = copyMoney._rubles;
            _kopeks = copyMoney._kopeks;
        }

        // Вспомогательный метод для нормализации рублей и копеек
        private void NormalizeValues(ref uint rubles, ref byte kopeks)
        {
            // Добавляем рубли за каждые 100 копеек
            rubles += (uint)(kopeks / 100);
            kopeks = (byte)(kopeks % 100);
        }

        // Свойства с автоматической нормализацией при установке
        public uint Rubles
        {
            get { return _rubles; }
            set { _rubles = value; }
        }

        public byte Kopeks
        {
            get { return _kopeks; }
            set
            {
                uint rublesTemp = _rubles;
                byte kopeksTemp = value;
                NormalizeValues(ref rublesTemp, ref kopeksTemp);
                _rubles = rublesTemp;
                _kopeks = kopeksTemp;
            }
        }

        public Money Subtract(Money other)
        {
            long totalGrosh = (long)_rubles * 100 + _kopeks;
            long otherTotalGrosh = (long)other._rubles * 100 + other._kopeks;
            long resultTotalGrosh = totalGrosh - otherTotalGrosh;

            if (resultTotalGrosh < 0)
            {
                throw new InvalidOperationException("ВЫ ВЗЯЛИ кредит");
            }

            uint resultRubles = (uint)(resultTotalGrosh / 100);
            byte resultKopeks = (byte)(resultTotalGrosh % 100);

            return new Money(resultRubles, resultKopeks);
        }

        public static Money operator ++(Money m)
        {
            int newTotalKopeks = (int)(m._rubles * 100 + m._kopeks + 1);
            uint newRubles = (uint)(newTotalKopeks / 100);
            byte newKopeks = (byte)(newTotalKopeks % 100);
            return new Money(newRubles, newKopeks);
        }

        public static Money operator --(Money m)
        {
            int totalKopeks = (int)(m._rubles * 100 + m._kopeks);

            if (totalKopeks <= 0)
            {
                throw new InvalidOperationException("Нельзя взять кредит");
            }

            int newTotalKopeks = totalKopeks - 1;
            uint newRubles = (uint)(newTotalKopeks / 100);
            byte newKopeks = (byte)(newTotalKopeks % 100);
            return new Money(newRubles, newKopeks);
        }

        public static Money operator -(Money m, uint number)
        {
            long totalKopeks = (long)m._rubles * 100 + m._kopeks;
            long subtractKopeks = (long)number * 100;
            long resultTotalGrosh = totalKopeks - subtractKopeks;

            if (resultTotalGrosh < 0)
            {
                throw new InvalidOperationException("Ошибка: отрицательный результат");
            }

            uint resultRubles = (uint)(resultTotalGrosh / 100);
            byte resultKopeks = (byte)(resultTotalGrosh % 100);

            return new Money(resultRubles, resultKopeks);
        }

        public static Money operator -(uint number, Money m)
        {
            long totalKopeks = (long)number * 100;
            long subtractKopeks = (long)m._rubles * 100 + m._kopeks;
            long resultTotalGrosh = totalKopeks - subtractKopeks;

            if (resultTotalGrosh < 0)
            {
                throw new InvalidOperationException("Ошибка: отрицательный результат");
            }

            uint resultRubles = (uint)(resultTotalGrosh / 100);
            byte resultKopeks = (byte)(resultTotalGrosh % 100);

            return new Money(resultRubles, resultKopeks);
        }

        public static Money operator -(Money m1, Money m2)
        {
            return m1.Subtract(m2);
        }

        public override string ToString()
        {
            return $"{_rubles} руб. {_kopeks} коп.";
        }
    }
}