using System.Collections.Generic;

namespace Utility
{
    public class IDManager : IIDManager
    {
        static int _nextID;
        readonly List<int> _returnedIDs = new();
        
        public IDManager()
        {
            _nextID = 0;
        }
        
        public int GetUniqueID()
        {
            if (_returnedIDs.Count <= 0)
                return _nextID++;

            var id = _returnedIDs[0];
            _returnedIDs.RemoveAt(0);
            return id;
        }

        public void ReturnID(int id)
        {
            _returnedIDs.Add(id);
        }
    }
}