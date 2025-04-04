using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.DataStructures
{
    public class VideoNode
    {
        public Video Video { get; set; }
        public VideoNode Left { get; set; }
        public VideoNode Right { get; set; }

        public VideoNode(Video video)
        {
            Video = video;
            Left = null;
            Right = null;
        }
    }

    public class VideoBST
    {
        private VideoNode _root;
        public VideoBST()
        {
            _root = null;
        }

        // Insert a new video
        public void Insert(Video video)
        {
            _root = InsertRec(_root, video);
        }

        private VideoNode InsertRec(VideoNode root, Video video)
        {
            if (root == null)
                return new VideoNode(video);

            if (video.ID < root.Video.ID)
                root.Left = InsertRec(root.Left, video);
            else if (video.ID > root.Video.ID)
                root.Right = InsertRec(root.Right, video);

            return root;
        }
    }
}
