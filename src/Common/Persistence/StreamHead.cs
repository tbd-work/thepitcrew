namespace Common.Persistence
{
    class StreamHead
    {
        public string Id { get; set; }
        public int Commit { get; set; }

        public void UpdateCommitTo(int commitId)
        {
            Commit = commitId;
        }

        public bool IsFirstCommit()
        {
            return Commit == 1;
        }
    }
}