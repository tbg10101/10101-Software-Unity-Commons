namespace Software10101.Serialization {
    public interface ISerializable {
        string Serialize();
        void Deserialize(string payload);
    }
}
