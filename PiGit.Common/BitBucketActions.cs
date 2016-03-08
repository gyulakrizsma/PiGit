using System.Collections.Generic;

namespace PiGit.Common
{
    public static class BitBucketActions
    {
        private const string RepoPushed = "repo:push";
        private const string CommentCreated = "pullrequest:comment_created";
        private const string PrCreated = "pullrequest:created";
        private const string PrMerged= "pullrequest:fulfilled";
        private const string PrUpdated = "pullrequest:updated";

        public static Dictionary<string, string> Actions => new Dictionary<string, string>
        {
            { CommentCreated, "CommentCreated" },
            { PrCreated, "PrCreated" },
            { PrMerged, "PrMerged" },
            { PrUpdated, "PrUpdated" },
            { RepoPushed, "RepoPushed" },
        };
    }
}
