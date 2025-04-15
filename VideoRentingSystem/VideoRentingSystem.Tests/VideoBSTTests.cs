using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Tests
{
    [TestClass]
    public class VideoBSTTests
    {
        private VideoBST videoBST;
        private Video video1;
        private Video video2;
        private Video video3;
        private Video video4;

        [TestInitialize]
        public void TestInitialize()
        {
            videoBST = new VideoBST();
            videoBST.Clear();

            video1 = new Video
            {
                ID = 10,
                Title = "The Shawshank Redemption",
                Genre = "Drama",
                ReleaseDate = new DateTime(1994, 10, 14),
                Availability = true
            };

            video2 = new Video
            {
                ID = 5,
                Title = "The Godfather",
                Genre = "Crime",
                ReleaseDate = new DateTime(1972, 3, 24),
                Availability = false
            };

            video3 = new Video
            {
                ID = 15,
                Title = "Pulp Fiction",
                Genre = "Crime",
                ReleaseDate = new DateTime(1994, 10, 14),
                Availability = true
            };

            video4 = new Video
            {
                ID = 7,
                Title = "The Dark Knight",
                Genre = "Action",
                ReleaseDate = new DateTime(2008, 7, 18),
                Availability = true
            };
        }

        [TestMethod]
        public void Instance_ShouldReturnSingletonInstance()
        {
            // Arrange
            var instance1 = VideoBST.Instance;
            var instance2 = VideoBST.Instance;

            // Assert
            Assert.AreSame(instance1, instance2, "Instance should return the same object");
        }

        [TestMethod]
        public void Insert_ShouldAddVideoToBST()
        {
            // Act
            videoBST.Insert(video1);

            // Assert
            var result = videoBST.Search(video1.ID);
            Assert.IsNotNull(result, "Video should be found after insertion");
            Assert.AreEqual(video1.Title, result.Title, "Titles should match");
        }

        [TestMethod]
        public void Insert_ShouldMaintainBSTProperty()
        {
            // Arrange
            videoBST.Insert(video1); // ID 10 (root)
            videoBST.Insert(video2); // ID 5 (left of root)
            videoBST.Insert(video3); // ID 15 (right of root)
            videoBST.Insert(video4); // ID 7 (right of 5)

            // Act
            var videos = new List<Video>();
            videoBST.InOrderTraversal(v => videos.Add(v));

            // Assert
            Assert.AreEqual(4, videos.Count, "Should contain all inserted videos");
            Assert.AreEqual(video2.ID, videos[0].ID, "First should be smallest ID");
            Assert.AreEqual(video4.ID, videos[1].ID, "Second should be next smallest");
            Assert.AreEqual(video1.ID, videos[2].ID, "Third should be next");
            Assert.AreEqual(video3.ID, videos[3].ID, "Last should be largest");
        }

        [TestMethod]
        public void Search_ShouldFindExistingVideo()
        {
            // Arrange
            videoBST.Insert(video1);
            videoBST.Insert(video2);

            // Act
            var result = videoBST.Search(video2.ID);

            // Assert
            Assert.IsNotNull(result, "Video should be found");
            Assert.AreEqual(video2.Title, result.Title, "Titles should match");
        }

        [TestMethod]
        public void Search_ShouldReturnNullForNonExistingVideo()
        {
            // Arrange
            videoBST.Insert(video1);

            // Act
            var result = videoBST.Search(999); // Non-existing ID

            // Assert
            Assert.IsNull(result, "Should return null for non-existing video");
        }

        [TestMethod]
        public void Update_ShouldModifyExistingVideo()
        {
            // Arrange
            videoBST.Insert(video1);
            var updatedVideo = new Video
            {
                ID = video1.ID,
                Title = "Updated Title",
                Genre = "Updated Genre",
                ReleaseDate = new DateTime(2000, 1, 1),
                Availability = false
            };

            // Act
            var result = videoBST.Update(video1.ID, updatedVideo);

            // Assert
            Assert.IsNotNull(result, "Should return updated node");
            var searchedVideo = videoBST.Search(video1.ID);
            Assert.AreEqual("Updated Title", searchedVideo.Title, "Title should be updated");
            Assert.AreEqual("Updated Genre", searchedVideo.Genre, "Genre should be updated");
            Assert.AreEqual(false, searchedVideo.Availability, "Availability should be updated");
        }

        [TestMethod]
        public void Update_ShouldReturnNullForNonExistingVideo()
        {
            // Arrange
            videoBST.Insert(video1);

            // Act
            var result = videoBST.Update(999, video1); // Non-existing ID

            // Assert
            Assert.IsNull(result, "Should return null for non-existing video");
        }

        [TestMethod]
        public void Delete_ShouldRemoveVideoWithNoChildren()
        {
            // Arrange
            videoBST.Insert(video1); // Root
            videoBST.Insert(video2); // Left child (no children)

            // Act
            bool isDeleted = videoBST.Delete(video2.ID);

            // Assert
            Assert.IsTrue(isDeleted, "Should return true for successful deletion");
            Assert.IsNull(videoBST.Search(video2.ID), "Video should no longer exist");
        }

        [TestMethod]
        public void Delete_ShouldRemoveVideoWithOneChild()
        {
            // Arrange
            videoBST.Insert(video1); // Root (10)
            videoBST.Insert(video2); // Left (5)
            videoBST.Insert(video4); // Right of 5 (7)

            // Act
            bool isDeleted = videoBST.Delete(video2.ID);

            // Assert
            Assert.IsTrue(isDeleted, "Should return true for successful deletion");
            Assert.IsNull(videoBST.Search(video2.ID), "Video should no longer exist");
            Assert.IsNotNull(videoBST.Search(video4.ID), "Child video should still exist");
        }

        [TestMethod]
        public void Delete_ShouldRemoveVideoWithTwoChildren()
        {
            // Arrange
            videoBST.Insert(video1); // Root (10)
            videoBST.Insert(video2); // Left (5)
            videoBST.Insert(video3); // Right (15)
            videoBST.Insert(video4); // Right of 5 (7)

            // Act
            bool isDeleted = videoBST.Delete(video1.ID);

            // Assert
            Assert.IsTrue(isDeleted, "Should return true for successful deletion");
            Assert.IsNull(videoBST.Search(video1.ID), "Video should no longer exist");

            // Verify BST structure is maintained
            var videos = new List<Video>();
            videoBST.InOrderTraversal(v => videos.Add(v));
            Assert.AreEqual(3, videos.Count, "Should have remaining videos");
            Assert.AreEqual(video2.ID, videos[0].ID, "First should be smallest");
            Assert.AreEqual(video4.ID, videos[1].ID, "Second should be next");
            Assert.AreEqual(video3.ID, videos[2].ID, "Last should be largest");
        }

        [TestMethod]
        public void Delete_ShouldReturnFalseForNonExistingVideo()
        {
            // Arrange
            videoBST.Insert(video1);

            // Act
            bool isDeleted = videoBST.Delete(999); // Non-existing ID

            // Assert
            Assert.IsFalse(isDeleted, "Should return false for non-existing video");
        }

        [TestMethod]
        public void InOrderTraversal_ShouldVisitAllNodesInOrder()
        {
            // Arrange
            videoBST.Insert(video1); // 10
            videoBST.Insert(video2); // 5
            videoBST.Insert(video3); // 15
            videoBST.Insert(video4); // 7

            var visitedIds = new List<int>();

            // Act
            videoBST.InOrderTraversal(v => visitedIds.Add(v.ID));

            // Assert
            Assert.AreEqual(4, visitedIds.Count, "Should visit all nodes");
            Assert.AreEqual(video2.ID, visitedIds[0], "First should be smallest ID (5)");
            Assert.AreEqual(video4.ID, visitedIds[1], "Second should be next (7)");
            Assert.AreEqual(video1.ID, visitedIds[2], "Third should be next (10)");
            Assert.AreEqual(video3.ID, visitedIds[3], "Last should be largest (15)");
        }

        [TestMethod]
        public void DisplayAllVideos_ShouldNotThrowException()
        {
            // Arrange
            videoBST.Insert(video1);
            videoBST.Insert(video2);

            // Act & Assert
            try
            {
                videoBST.DisplayAllVideos();
            }
            catch
            {
                Assert.Fail("DisplayAllVideos should not throw exceptions");
            }
        }

        [TestMethod]
        public void Clear_ShouldEmptyTheBST()
        {
            // Arrange
            videoBST.Insert(video1);
            videoBST.Insert(video2);

            // Act
            videoBST.Clear();

            // Assert
            Assert.IsNull(videoBST.Search(video1.ID), "Video1 should be removed");
            Assert.IsNull(videoBST.Search(video2.ID), "Video2 should be removed");
        }
    }
}
