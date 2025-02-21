public interface IObserver {
    void OnNotify(string eventType, object data);
}

public interface ISubject {
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers(string eventType, object data);
}
