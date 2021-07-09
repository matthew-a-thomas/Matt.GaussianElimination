namespace Matt.GaussianElimination
{
    readonly unsafe struct BytePointer
    {
        public readonly byte* Pointer;

        public BytePointer(byte* pointer)
        {
            Pointer = pointer;
        }
    }
}