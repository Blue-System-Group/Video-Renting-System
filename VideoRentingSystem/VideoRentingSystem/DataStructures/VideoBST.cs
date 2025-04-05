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

        // Insert a new video recursively
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

        // Update a video
        public VideoNode Update(int videoId, Video video)
        {
            VideoNode updatedNode = null;
            _root = UpdateRec(_root, videoId, video, ref updatedNode);
            return updatedNode;
        }

        // Update a video recursively
        private VideoNode UpdateRec(VideoNode root, int videoId, Video video, ref VideoNode updatedNode)
        {
            if (root == null)
                return null;  // Video not found
            if (videoId < root.Video.ID)
                root.Left = UpdateRec(root.Left, videoId, video, ref updatedNode);
            else if (videoId > root.Video.ID)
                root.Right = UpdateRec(root.Right, videoId, video, ref updatedNode);
            else
            {
                // Found the video
                root.Video = video;
                updatedNode = root;
            }
            return root;
        }

        // Delete a video
        public bool Delete(int videoId)
        {
            bool isDeleted = false;
            _root = DeleteRec(_root, videoId, ref isDeleted);
            return isDeleted;
        }

        // Delete a video recursively
        private VideoNode DeleteRec(VideoNode root, int videoId, ref bool isDeleted)
        {
            if (root == null)
                return null;  // Video not found

            if (videoId < root.Video.ID)
                root.Left = DeleteRec(root.Left, videoId, ref isDeleted);
            else if (videoId > root.Video.ID)
                root.Right = DeleteRec(root.Right, videoId, ref isDeleted);
            else
            {
                // Found the video
                isDeleted = true;

                // Node with only one child or no child
                if (root.Left == null)
                    return root.Right;
                if (root.Right == null)
                    return root.Left;

                // Node with two children
                root.Video = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Video.ID, ref isDeleted);
            }

            return root;
        }

        private Video MinValue(VideoNode root)
        {
            while (root.Left != null)
                root = root.Left;
            return root.Video;
        }
    }
}
