using System;

namespace AdventOfCode2020.Day25
{
    public class DayTwentyfive : IDay
    {
        private long CardPublicKey;
        private long DoorPublicKey;

        public DayTwentyfive()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {            
            var cardLoopSize = FindLoopSize(CardPublicKey);
            var doorLoopSize = FindLoopSize(DoorPublicKey);
            
            var encryptionKeyCard = FindEncryptionKey(cardLoopSize, DoorPublicKey);
            var encryptionKeyDoor = FindEncryptionKey(doorLoopSize, CardPublicKey);

            Console.WriteLine($"Puzzle 1 solution: {encryptionKeyCard} - Similar results? -> {encryptionKeyCard == encryptionKeyDoor}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            CardPublicKey = 8335663;
            DoorPublicKey = 8614349;
        }

        private long FindLoopSize(long publicKey)
        {
            long value = 1;
            long loops = 0;
            while (value != publicKey)
            {
                loops++;
                // Set the value to itself multiplied by the subject number.
                value *= 7;
                // Set the value to the remainder after dividing the value by 20201227.
                value %= 20201227;
            }
            return loops;
        }

        private long FindEncryptionKey(long loopSize, long publicKey)
        {
            long value = 1;
            for (int i = 0; i < loopSize; i++)
            {
                // Set the value to itself multiplied by the subject number.
                value *= publicKey;
                // Set the value to the remainder after dividing the value by 20201227.
                value %= 20201227;
            }
            return value;
        }
    }
}