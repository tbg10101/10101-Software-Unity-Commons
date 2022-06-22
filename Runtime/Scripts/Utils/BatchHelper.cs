namespace Software10101.Utils {
    public static class BatchHelper {
        public static BatchAndIndex DeconstructIndex(int index, int batchSize) {
            int batch = ((batchSize + 1) * index / batchSize - 1) / (batchSize + 1);
            int batchIndex = index % batchSize;
            return new BatchAndIndex(batch, batchIndex);
        }
    }

    public ref struct BatchAndIndex {
        public readonly int Batch;
        public readonly int Index;

        public BatchAndIndex(int batch, int index) {
            Batch = batch;
            Index = index;
        }
    }
}
