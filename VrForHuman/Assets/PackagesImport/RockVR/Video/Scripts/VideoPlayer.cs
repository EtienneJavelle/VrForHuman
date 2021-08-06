﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace RockVR.Video {
    public class VideoPlayer : MonoBehaviour {
#if UNITY_5_6_OR_NEWER
        /// <summary>
        /// Save the video files.
        /// </summary>
        public List<string> videoFiles = new List<string>();
        public List<string> currentVideoFiles = new List<string>();
        /// <summary>
        /// Play video properties.
        /// </summary>
        private UnityEngine.Video.VideoPlayer videoPlayerImpl;
        private int index = 0;
        public Transform videoPanel { get; set; }
        public bool videoPlayerActive { get; set; }
        public bool saving { get; set; }

        public static VideoPlayer instance;
        private void Awake() {
            if(instance == null) {
                instance = this;
            }
        }

        private void Update() {
            if(videoPlayerImpl != null && videoPlayerImpl.isPlaying == false && videoPlayerActive) {
                videoPlayerActive = false;
                //SetVideoPlayerImplCameraTarget(null);
                //Camera.main.GetComponent<CameraManager>().SetActiveReplayVideoCanvas(false);
                videoPlayerImpl.GetComponent<RawImage>().enabled = false;
                GameManager.Instance.replayVideoIsPlaying = false;
            }
        }

        public void SetVideoPlayerImplCameraTarget(Camera _cam) {
            videoPlayerImpl.targetCamera = _cam;
        }

        public void SetParentVideoPlayerImpl() {
            videoPlayerImpl.transform.SetParent(videoPanel.parent);
            videoPlayerImpl.transform.localPosition = videoPanel.localPosition;
            videoPlayerImpl.transform.GetComponent<RectTransform>().localRotation = videoPanel.transform.GetComponent<RectTransform>().localRotation;
        }

        /// <summary>
        /// Add video file to video file list.
        /// </summary>
        public void SetRootFolder() {
            if(Directory.Exists(PathConfig.SaveFolder)) {
                DirectoryInfo direction = new DirectoryInfo(PathConfig.SaveFolder);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                videoFiles.Clear();
                for(int i = 0; i < files.Length; i++) {
                    if(files[i].Name.EndsWith(".mp4")) {
                        videoFiles.Add(PathConfig.SaveFolder + files[i].Name);
                        continue;
                    }
                }
            }
            // Init VideoPlayer properties.
            //videoPlayerImpl = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayerImpl = FindObjectOfType<UnityEngine.Video.VideoPlayer>();
            videoPlayerImpl.source = UnityEngine.Video.VideoSource.Url;
            videoPlayerImpl.playOnAwake = false;
            //videoPlayerImpl.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            //SetVideoPlayerImplCameraTarget(Camera.main);
            videoPlayerImpl.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
            videoPlayerImpl.controlledAudioTrackCount = 1;
            videoPlayerImpl.aspectRatio = UnityEngine.Video.VideoAspectRatio.Stretch;
            if(gameObject.GetComponent<AudioSource>() != null) {
                videoPlayerImpl.SetTargetAudioSource(0, gameObject.GetComponent<AudioSource>());
                gameObject.GetComponent<AudioSource>().clip = null;
            }
        }
        /// <summary>
        /// Play video process.
        /// </summary>
        public void PlayVideo() {
            if(index >= videoFiles.Count) {
                return;
            }

            videoPlayerImpl.url = "file://" + videoFiles[index];
            videoPlayerImpl.GetComponent<RawImage>().enabled = true;
            Debug.Log("[VideoPlayer::PlayVideo] Video Path:" + "video : " + index + " " + videoFiles[index]);
            videoPlayerImpl.Play();
        }

        public void PlayVideoAtIndex(int _index) {
            if(_index >= currentVideoFiles.Count) {
                return;
            }

            videoPlayerImpl.url = "file://" + currentVideoFiles[_index];
            videoPlayerImpl.GetComponent<RawImage>().enabled = true;
            Debug.Log("[VideoPlayer::PlayVideo] Video Path:" + "video : " + _index + " " + currentVideoFiles[_index]);
            videoPlayerImpl.Play();
        }

        /// <summary>
        /// Turn to next video
        /// </summary>
        public void NextVideo() {
            if(index < videoFiles.Count) {
                index++;
            } else {
                Debug.LogWarning("[VideoPlayer::NextVideo] All videos have already been played.");
            }
        }

        public void PreviousVideo() {
            if(index > 0) {
                index--;
                // Play capture video.
                PlayVideo();
            } else {
                Debug.LogWarning("[VideoPlayer::PreviousVideo] First video already selected.");
            }
        }

        public void ExitVideoMode() {
            videoPlayerImpl.targetCamera = null;
            videoPlayerImpl.GetComponent<RawImage>().enabled = false;
        }


#endif
    }
}
