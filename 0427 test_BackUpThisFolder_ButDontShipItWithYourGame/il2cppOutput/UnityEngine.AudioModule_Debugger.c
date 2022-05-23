#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodExecutionContextInfo g_methodExecutionContextInfos[1] = { { 0, 0, 0 } };
#else
static const Il2CppMethodExecutionContextInfo g_methodExecutionContextInfos[1] = { { 0, 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const char* g_methodExecutionContextInfoStrings[1] = { NULL };
#else
static const char* g_methodExecutionContextInfoStrings[1] = { NULL };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodExecutionContextInfoIndex g_methodExecutionContextInfoIndexes[48] = 
{
	{ 0, 0 } /* 0x06000001 System.Void UnityEngine.AudioSettings::InvokeOnAudioConfigurationChanged(System.Boolean) */,
	{ 0, 0 } /* 0x06000002 System.Void UnityEngine.AudioSettings::InvokeOnAudioSystemShuttingDown() */,
	{ 0, 0 } /* 0x06000003 System.Void UnityEngine.AudioSettings::InvokeOnAudioSystemStartedUp() */,
	{ 0, 0 } /* 0x06000004 System.Boolean UnityEngine.AudioSettings::StartAudioOutput() */,
	{ 0, 0 } /* 0x06000005 System.Boolean UnityEngine.AudioSettings::StopAudioOutput() */,
	{ 0, 0 } /* 0x06000006 System.Void UnityEngine.AudioSettings/AudioConfigurationChangeHandler::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0 } /* 0x06000007 System.Void UnityEngine.AudioSettings/AudioConfigurationChangeHandler::Invoke(System.Boolean) */,
	{ 0, 0 } /* 0x06000008 System.Boolean UnityEngine.AudioSettings/Mobile::get_muteState() */,
	{ 0, 0 } /* 0x06000009 System.Void UnityEngine.AudioSettings/Mobile::set_muteState(System.Boolean) */,
	{ 0, 0 } /* 0x0600000A System.Boolean UnityEngine.AudioSettings/Mobile::get_stopAudioOutputOnMute() */,
	{ 0, 0 } /* 0x0600000B System.Void UnityEngine.AudioSettings/Mobile::InvokeOnMuteStateChanged(System.Boolean) */,
	{ 0, 0 } /* 0x0600000C System.Void UnityEngine.AudioSettings/Mobile::StartAudioOutput() */,
	{ 0, 0 } /* 0x0600000D System.Void UnityEngine.AudioSettings/Mobile::StopAudioOutput() */,
	{ 0, 0 } /* 0x0600000E System.Void UnityEngine.AudioClip::.ctor() */,
	{ 0, 0 } /* 0x0600000F System.Void UnityEngine.AudioClip::InvokePCMReaderCallback_Internal(System.Single[]) */,
	{ 0, 0 } /* 0x06000010 System.Void UnityEngine.AudioClip::InvokePCMSetPositionCallback_Internal(System.Int32) */,
	{ 0, 0 } /* 0x06000011 System.Void UnityEngine.AudioClip/PCMReaderCallback::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0 } /* 0x06000012 System.Void UnityEngine.AudioClip/PCMReaderCallback::Invoke(System.Single[]) */,
	{ 0, 0 } /* 0x06000013 System.Void UnityEngine.AudioClip/PCMSetPositionCallback::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0 } /* 0x06000014 System.Void UnityEngine.AudioClip/PCMSetPositionCallback::Invoke(System.Int32) */,
	{ 0, 0 } /* 0x06000015 System.Single UnityEngine.AudioListener::get_volume() */,
	{ 0, 0 } /* 0x06000016 System.Void UnityEngine.AudioListener::set_volume(System.Single) */,
	{ 0, 0 } /* 0x06000017 System.Single UnityEngine.AudioSource::GetPitch(UnityEngine.AudioSource) */,
	{ 0, 0 } /* 0x06000018 System.Void UnityEngine.AudioSource::SetPitch(UnityEngine.AudioSource,System.Single) */,
	{ 0, 0 } /* 0x06000019 System.Void UnityEngine.AudioSource::PlayHelper(UnityEngine.AudioSource,System.UInt64) */,
	{ 0, 0 } /* 0x0600001A System.Void UnityEngine.AudioSource::PlayOneShotHelper(UnityEngine.AudioSource,UnityEngine.AudioClip,System.Single) */,
	{ 0, 0 } /* 0x0600001B System.Void UnityEngine.AudioSource::Stop(System.Boolean) */,
	{ 0, 0 } /* 0x0600001C System.Single UnityEngine.AudioSource::get_volume() */,
	{ 0, 0 } /* 0x0600001D System.Void UnityEngine.AudioSource::set_volume(System.Single) */,
	{ 0, 0 } /* 0x0600001E System.Single UnityEngine.AudioSource::get_pitch() */,
	{ 0, 0 } /* 0x0600001F System.Void UnityEngine.AudioSource::set_pitch(System.Single) */,
	{ 0, 0 } /* 0x06000020 UnityEngine.AudioClip UnityEngine.AudioSource::get_clip() */,
	{ 0, 0 } /* 0x06000021 System.Void UnityEngine.AudioSource::set_clip(UnityEngine.AudioClip) */,
	{ 0, 0 } /* 0x06000022 System.Void UnityEngine.AudioSource::Play() */,
	{ 0, 0 } /* 0x06000023 System.Void UnityEngine.AudioSource::PlayOneShot(UnityEngine.AudioClip,System.Single) */,
	{ 0, 0 } /* 0x06000024 System.Void UnityEngine.AudioSource::Stop() */,
	{ 0, 0 } /* 0x06000025 System.Boolean UnityEngine.AudioSource::get_isPlaying() */,
	{ 0, 0 } /* 0x06000026 System.Void UnityEngine.AudioSource::set_loop(System.Boolean) */,
	{ 0, 0 } /* 0x06000027 UnityEngine.Playables.PlayableHandle UnityEngine.Audio.AudioClipPlayable::GetHandle() */,
	{ 0, 0 } /* 0x06000028 System.Boolean UnityEngine.Audio.AudioClipPlayable::Equals(UnityEngine.Audio.AudioClipPlayable) */,
	{ 0, 0 } /* 0x06000029 System.Boolean UnityEngine.Audio.AudioMixer::SetFloat(System.String,System.Single) */,
	{ 0, 0 } /* 0x0600002A System.Boolean UnityEngine.Audio.AudioMixer::GetFloat(System.String,System.Single&) */,
	{ 0, 0 } /* 0x0600002B UnityEngine.Playables.PlayableHandle UnityEngine.Audio.AudioMixerPlayable::GetHandle() */,
	{ 0, 0 } /* 0x0600002C System.Boolean UnityEngine.Audio.AudioMixerPlayable::Equals(UnityEngine.Audio.AudioMixerPlayable) */,
	{ 0, 0 } /* 0x0600002D System.Void UnityEngine.Experimental.Audio.AudioSampleProvider::InvokeSampleFramesAvailable(System.Int32) */,
	{ 0, 0 } /* 0x0600002E System.Void UnityEngine.Experimental.Audio.AudioSampleProvider::InvokeSampleFramesOverflow(System.Int32) */,
	{ 0, 0 } /* 0x0600002F System.Void UnityEngine.Experimental.Audio.AudioSampleProvider/SampleFramesHandler::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0 } /* 0x06000030 System.Void UnityEngine.Experimental.Audio.AudioSampleProvider/SampleFramesHandler::Invoke(UnityEngine.Experimental.Audio.AudioSampleProvider,System.UInt32) */,
};
#else
static const Il2CppMethodExecutionContextInfoIndex g_methodExecutionContextInfoIndexes[1] = { { 0, 0} };
#endif
#if IL2CPP_MONO_DEBUGGER
IL2CPP_EXTERN_C Il2CppSequencePoint g_sequencePointsUnityEngine_AudioModule[];
Il2CppSequencePoint g_sequencePointsUnityEngine_AudioModule[170] = 
{
	{ 52072, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 0 } /* seqPointIndex: 0 */,
	{ 52072, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 1 } /* seqPointIndex: 1 */,
	{ 52072, 1, 342, 342, 9, 10, 0, kSequencePointKind_Normal, 0, 2 } /* seqPointIndex: 2 */,
	{ 52072, 1, 343, 343, 13, 53, 1, kSequencePointKind_Normal, 0, 3 } /* seqPointIndex: 3 */,
	{ 52072, 1, 343, 343, 0, 0, 10, kSequencePointKind_Normal, 0, 4 } /* seqPointIndex: 4 */,
	{ 52072, 1, 344, 344, 17, 63, 13, kSequencePointKind_Normal, 0, 5 } /* seqPointIndex: 5 */,
	{ 52072, 1, 344, 344, 17, 63, 19, kSequencePointKind_StepOut, 0, 6 } /* seqPointIndex: 6 */,
	{ 52072, 1, 345, 345, 9, 10, 25, kSequencePointKind_Normal, 0, 7 } /* seqPointIndex: 7 */,
	{ 52073, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 8 } /* seqPointIndex: 8 */,
	{ 52073, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 9 } /* seqPointIndex: 9 */,
	{ 52073, 1, 349, 349, 16, 51, 0, kSequencePointKind_Normal, 0, 10 } /* seqPointIndex: 10 */,
	{ 52073, 1, 349, 349, 16, 51, 11, kSequencePointKind_StepOut, 0, 11 } /* seqPointIndex: 11 */,
	{ 52074, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 12 } /* seqPointIndex: 12 */,
	{ 52074, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 13 } /* seqPointIndex: 13 */,
	{ 52074, 1, 353, 353, 16, 48, 0, kSequencePointKind_Normal, 0, 14 } /* seqPointIndex: 14 */,
	{ 52074, 1, 353, 353, 16, 48, 11, kSequencePointKind_StepOut, 0, 15 } /* seqPointIndex: 15 */,
	{ 52079, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 16 } /* seqPointIndex: 16 */,
	{ 52079, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 17 } /* seqPointIndex: 17 */,
	{ 52079, 1, 378, 378, 17, 21, 0, kSequencePointKind_Normal, 0, 18 } /* seqPointIndex: 18 */,
	{ 52080, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 19 } /* seqPointIndex: 19 */,
	{ 52080, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 20 } /* seqPointIndex: 20 */,
	{ 52080, 1, 379, 379, 17, 29, 0, kSequencePointKind_Normal, 0, 21 } /* seqPointIndex: 21 */,
	{ 52081, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 22 } /* seqPointIndex: 22 */,
	{ 52081, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 23 } /* seqPointIndex: 23 */,
	{ 52081, 1, 386, 386, 17, 18, 0, kSequencePointKind_Normal, 0, 24 } /* seqPointIndex: 24 */,
	{ 52081, 1, 387, 387, 21, 51, 1, kSequencePointKind_Normal, 0, 25 } /* seqPointIndex: 25 */,
	{ 52081, 1, 388, 388, 17, 18, 9, kSequencePointKind_Normal, 0, 26 } /* seqPointIndex: 26 */,
	{ 52082, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 27 } /* seqPointIndex: 27 */,
	{ 52082, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 28 } /* seqPointIndex: 28 */,
	{ 52082, 1, 411, 411, 13, 14, 0, kSequencePointKind_Normal, 0, 29 } /* seqPointIndex: 29 */,
	{ 52082, 1, 412, 412, 17, 39, 1, kSequencePointKind_Normal, 0, 30 } /* seqPointIndex: 30 */,
	{ 52082, 1, 412, 412, 17, 39, 2, kSequencePointKind_StepOut, 0, 31 } /* seqPointIndex: 31 */,
	{ 52082, 1, 412, 412, 0, 0, 13, kSequencePointKind_Normal, 0, 32 } /* seqPointIndex: 32 */,
	{ 52082, 1, 413, 413, 17, 18, 16, kSequencePointKind_Normal, 0, 33 } /* seqPointIndex: 33 */,
	{ 52082, 1, 414, 414, 21, 38, 17, kSequencePointKind_Normal, 0, 34 } /* seqPointIndex: 34 */,
	{ 52082, 1, 414, 414, 21, 38, 18, kSequencePointKind_StepOut, 0, 35 } /* seqPointIndex: 35 */,
	{ 52082, 1, 415, 415, 21, 47, 24, kSequencePointKind_Normal, 0, 36 } /* seqPointIndex: 36 */,
	{ 52082, 1, 415, 415, 21, 47, 24, kSequencePointKind_StepOut, 0, 37 } /* seqPointIndex: 37 */,
	{ 52082, 1, 415, 415, 0, 0, 30, kSequencePointKind_Normal, 0, 38 } /* seqPointIndex: 38 */,
	{ 52082, 1, 416, 416, 21, 22, 33, kSequencePointKind_Normal, 0, 39 } /* seqPointIndex: 39 */,
	{ 52082, 1, 417, 417, 25, 39, 34, kSequencePointKind_Normal, 0, 40 } /* seqPointIndex: 40 */,
	{ 52082, 1, 417, 417, 25, 39, 34, kSequencePointKind_StepOut, 0, 41 } /* seqPointIndex: 41 */,
	{ 52082, 1, 417, 417, 0, 0, 40, kSequencePointKind_Normal, 0, 42 } /* seqPointIndex: 42 */,
	{ 52082, 1, 418, 418, 29, 47, 43, kSequencePointKind_Normal, 0, 43 } /* seqPointIndex: 43 */,
	{ 52082, 1, 418, 418, 29, 47, 43, kSequencePointKind_StepOut, 0, 44 } /* seqPointIndex: 44 */,
	{ 52082, 1, 418, 418, 0, 0, 49, kSequencePointKind_Normal, 0, 45 } /* seqPointIndex: 45 */,
	{ 52082, 1, 420, 420, 29, 48, 51, kSequencePointKind_Normal, 0, 46 } /* seqPointIndex: 46 */,
	{ 52082, 1, 420, 420, 29, 48, 51, kSequencePointKind_StepOut, 0, 47 } /* seqPointIndex: 47 */,
	{ 52082, 1, 421, 421, 21, 22, 57, kSequencePointKind_Normal, 0, 48 } /* seqPointIndex: 48 */,
	{ 52082, 1, 422, 422, 21, 52, 58, kSequencePointKind_Normal, 0, 49 } /* seqPointIndex: 49 */,
	{ 52082, 1, 422, 422, 0, 0, 67, kSequencePointKind_Normal, 0, 50 } /* seqPointIndex: 50 */,
	{ 52082, 1, 423, 423, 25, 50, 70, kSequencePointKind_Normal, 0, 51 } /* seqPointIndex: 51 */,
	{ 52082, 1, 423, 423, 25, 50, 76, kSequencePointKind_StepOut, 0, 52 } /* seqPointIndex: 52 */,
	{ 52082, 1, 424, 424, 17, 18, 82, kSequencePointKind_Normal, 0, 53 } /* seqPointIndex: 53 */,
	{ 52082, 1, 425, 425, 13, 14, 83, kSequencePointKind_Normal, 0, 54 } /* seqPointIndex: 54 */,
	{ 52083, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 55 } /* seqPointIndex: 55 */,
	{ 52083, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 56 } /* seqPointIndex: 56 */,
	{ 52083, 1, 428, 428, 13, 14, 0, kSequencePointKind_Normal, 0, 57 } /* seqPointIndex: 57 */,
	{ 52083, 1, 429, 429, 17, 50, 1, kSequencePointKind_Normal, 0, 58 } /* seqPointIndex: 58 */,
	{ 52083, 1, 429, 429, 17, 50, 1, kSequencePointKind_StepOut, 0, 59 } /* seqPointIndex: 59 */,
	{ 52083, 1, 430, 430, 13, 14, 7, kSequencePointKind_Normal, 0, 60 } /* seqPointIndex: 60 */,
	{ 52084, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 61 } /* seqPointIndex: 61 */,
	{ 52084, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 62 } /* seqPointIndex: 62 */,
	{ 52084, 1, 433, 433, 13, 14, 0, kSequencePointKind_Normal, 0, 63 } /* seqPointIndex: 63 */,
	{ 52084, 1, 434, 434, 17, 49, 1, kSequencePointKind_Normal, 0, 64 } /* seqPointIndex: 64 */,
	{ 52084, 1, 434, 434, 17, 49, 1, kSequencePointKind_StepOut, 0, 65 } /* seqPointIndex: 65 */,
	{ 52084, 1, 435, 435, 13, 14, 7, kSequencePointKind_Normal, 0, 66 } /* seqPointIndex: 66 */,
	{ 52085, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 67 } /* seqPointIndex: 67 */,
	{ 52085, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 68 } /* seqPointIndex: 68 */,
	{ 52085, 1, 617, 617, 9, 68, 0, kSequencePointKind_Normal, 0, 69 } /* seqPointIndex: 69 */,
	{ 52085, 1, 621, 621, 9, 78, 7, kSequencePointKind_Normal, 0, 70 } /* seqPointIndex: 70 */,
	{ 52085, 1, 482, 482, 9, 28, 14, kSequencePointKind_Normal, 0, 71 } /* seqPointIndex: 71 */,
	{ 52085, 1, 482, 482, 9, 28, 15, kSequencePointKind_StepOut, 0, 72 } /* seqPointIndex: 72 */,
	{ 52085, 1, 482, 482, 29, 30, 21, kSequencePointKind_Normal, 0, 73 } /* seqPointIndex: 73 */,
	{ 52085, 1, 482, 482, 30, 31, 22, kSequencePointKind_Normal, 0, 74 } /* seqPointIndex: 74 */,
	{ 52086, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 75 } /* seqPointIndex: 75 */,
	{ 52086, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 76 } /* seqPointIndex: 76 */,
	{ 52086, 1, 625, 625, 9, 10, 0, kSequencePointKind_Normal, 0, 77 } /* seqPointIndex: 77 */,
	{ 52086, 1, 626, 626, 13, 45, 1, kSequencePointKind_Normal, 0, 78 } /* seqPointIndex: 78 */,
	{ 52086, 1, 626, 626, 0, 0, 11, kSequencePointKind_Normal, 0, 79 } /* seqPointIndex: 79 */,
	{ 52086, 1, 627, 627, 17, 43, 14, kSequencePointKind_Normal, 0, 80 } /* seqPointIndex: 80 */,
	{ 52086, 1, 627, 627, 17, 43, 21, kSequencePointKind_StepOut, 0, 81 } /* seqPointIndex: 81 */,
	{ 52086, 1, 628, 628, 9, 10, 27, kSequencePointKind_Normal, 0, 82 } /* seqPointIndex: 82 */,
	{ 52087, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 83 } /* seqPointIndex: 83 */,
	{ 52087, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 84 } /* seqPointIndex: 84 */,
	{ 52087, 1, 632, 632, 9, 10, 0, kSequencePointKind_Normal, 0, 85 } /* seqPointIndex: 85 */,
	{ 52087, 1, 633, 633, 13, 50, 1, kSequencePointKind_Normal, 0, 86 } /* seqPointIndex: 86 */,
	{ 52087, 1, 633, 633, 0, 0, 11, kSequencePointKind_Normal, 0, 87 } /* seqPointIndex: 87 */,
	{ 52087, 1, 634, 634, 17, 52, 14, kSequencePointKind_Normal, 0, 88 } /* seqPointIndex: 88 */,
	{ 52087, 1, 634, 634, 17, 52, 21, kSequencePointKind_StepOut, 0, 89 } /* seqPointIndex: 89 */,
	{ 52087, 1, 635, 635, 9, 10, 27, kSequencePointKind_Normal, 0, 90 } /* seqPointIndex: 90 */,
	{ 52101, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 91 } /* seqPointIndex: 91 */,
	{ 52101, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 92 } /* seqPointIndex: 92 */,
	{ 52101, 1, 729, 729, 17, 18, 0, kSequencePointKind_Normal, 0, 93 } /* seqPointIndex: 93 */,
	{ 52101, 1, 729, 729, 19, 41, 1, kSequencePointKind_Normal, 0, 94 } /* seqPointIndex: 94 */,
	{ 52101, 1, 729, 729, 19, 41, 2, kSequencePointKind_StepOut, 0, 95 } /* seqPointIndex: 95 */,
	{ 52101, 1, 729, 729, 42, 43, 10, kSequencePointKind_Normal, 0, 96 } /* seqPointIndex: 96 */,
	{ 52102, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 97 } /* seqPointIndex: 97 */,
	{ 52102, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 98 } /* seqPointIndex: 98 */,
	{ 52102, 1, 730, 730, 17, 18, 0, kSequencePointKind_Normal, 0, 99 } /* seqPointIndex: 99 */,
	{ 52102, 1, 730, 730, 19, 41, 1, kSequencePointKind_Normal, 0, 100 } /* seqPointIndex: 100 */,
	{ 52102, 1, 730, 730, 19, 41, 3, kSequencePointKind_StepOut, 0, 101 } /* seqPointIndex: 101 */,
	{ 52102, 1, 730, 730, 42, 43, 9, kSequencePointKind_Normal, 0, 102 } /* seqPointIndex: 102 */,
	{ 52105, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 103 } /* seqPointIndex: 103 */,
	{ 52105, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 104 } /* seqPointIndex: 104 */,
	{ 52105, 1, 833, 833, 9, 10, 0, kSequencePointKind_Normal, 0, 105 } /* seqPointIndex: 105 */,
	{ 52105, 1, 834, 834, 13, 33, 1, kSequencePointKind_Normal, 0, 106 } /* seqPointIndex: 106 */,
	{ 52105, 1, 834, 834, 13, 33, 4, kSequencePointKind_StepOut, 0, 107 } /* seqPointIndex: 107 */,
	{ 52105, 1, 835, 835, 9, 10, 10, kSequencePointKind_Normal, 0, 108 } /* seqPointIndex: 108 */,
	{ 52106, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 109 } /* seqPointIndex: 109 */,
	{ 52106, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 110 } /* seqPointIndex: 110 */,
	{ 52106, 1, 862, 862, 9, 10, 0, kSequencePointKind_Normal, 0, 111 } /* seqPointIndex: 111 */,
	{ 52106, 1, 863, 863, 13, 30, 1, kSequencePointKind_Normal, 0, 112 } /* seqPointIndex: 112 */,
	{ 52106, 1, 863, 863, 13, 30, 3, kSequencePointKind_StepOut, 0, 113 } /* seqPointIndex: 113 */,
	{ 52106, 1, 863, 863, 0, 0, 9, kSequencePointKind_Normal, 0, 114 } /* seqPointIndex: 114 */,
	{ 52106, 1, 864, 864, 13, 14, 12, kSequencePointKind_Normal, 0, 115 } /* seqPointIndex: 115 */,
	{ 52106, 1, 865, 865, 17, 83, 13, kSequencePointKind_Normal, 0, 116 } /* seqPointIndex: 116 */,
	{ 52106, 1, 865, 865, 17, 83, 18, kSequencePointKind_StepOut, 0, 117 } /* seqPointIndex: 117 */,
	{ 52106, 1, 866, 866, 17, 24, 24, kSequencePointKind_Normal, 0, 118 } /* seqPointIndex: 118 */,
	{ 52106, 1, 869, 869, 13, 56, 26, kSequencePointKind_Normal, 0, 119 } /* seqPointIndex: 119 */,
	{ 52106, 1, 869, 869, 13, 56, 29, kSequencePointKind_StepOut, 0, 120 } /* seqPointIndex: 120 */,
	{ 52106, 1, 870, 870, 9, 10, 35, kSequencePointKind_Normal, 0, 121 } /* seqPointIndex: 121 */,
	{ 52107, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 122 } /* seqPointIndex: 122 */,
	{ 52107, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 123 } /* seqPointIndex: 123 */,
	{ 52107, 1, 877, 877, 9, 10, 0, kSequencePointKind_Normal, 0, 124 } /* seqPointIndex: 124 */,
	{ 52107, 1, 878, 878, 13, 24, 1, kSequencePointKind_Normal, 0, 125 } /* seqPointIndex: 125 */,
	{ 52107, 1, 878, 878, 13, 24, 3, kSequencePointKind_StepOut, 0, 126 } /* seqPointIndex: 126 */,
	{ 52107, 1, 879, 879, 9, 10, 9, kSequencePointKind_Normal, 0, 127 } /* seqPointIndex: 127 */,
	{ 52110, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 128 } /* seqPointIndex: 128 */,
	{ 52110, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 129 } /* seqPointIndex: 129 */,
	{ 52110, 2, 49, 49, 9, 10, 0, kSequencePointKind_Normal, 0, 130 } /* seqPointIndex: 130 */,
	{ 52110, 2, 50, 50, 13, 29, 1, kSequencePointKind_Normal, 0, 131 } /* seqPointIndex: 131 */,
	{ 52110, 2, 51, 51, 9, 10, 10, kSequencePointKind_Normal, 0, 132 } /* seqPointIndex: 132 */,
	{ 52111, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 133 } /* seqPointIndex: 133 */,
	{ 52111, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 134 } /* seqPointIndex: 134 */,
	{ 52111, 2, 64, 64, 9, 10, 0, kSequencePointKind_Normal, 0, 135 } /* seqPointIndex: 135 */,
	{ 52111, 2, 65, 65, 13, 53, 1, kSequencePointKind_Normal, 0, 136 } /* seqPointIndex: 136 */,
	{ 52111, 2, 65, 65, 13, 53, 2, kSequencePointKind_StepOut, 0, 137 } /* seqPointIndex: 137 */,
	{ 52111, 2, 65, 65, 13, 53, 9, kSequencePointKind_StepOut, 0, 138 } /* seqPointIndex: 138 */,
	{ 52111, 2, 65, 65, 13, 53, 14, kSequencePointKind_StepOut, 0, 139 } /* seqPointIndex: 139 */,
	{ 52111, 2, 66, 66, 9, 10, 22, kSequencePointKind_Normal, 0, 140 } /* seqPointIndex: 140 */,
	{ 52114, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 141 } /* seqPointIndex: 141 */,
	{ 52114, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 142 } /* seqPointIndex: 142 */,
	{ 52114, 3, 47, 47, 9, 10, 0, kSequencePointKind_Normal, 0, 143 } /* seqPointIndex: 143 */,
	{ 52114, 3, 48, 48, 13, 29, 1, kSequencePointKind_Normal, 0, 144 } /* seqPointIndex: 144 */,
	{ 52114, 3, 49, 49, 9, 10, 10, kSequencePointKind_Normal, 0, 145 } /* seqPointIndex: 145 */,
	{ 52115, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 146 } /* seqPointIndex: 146 */,
	{ 52115, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 147 } /* seqPointIndex: 147 */,
	{ 52115, 3, 62, 62, 9, 10, 0, kSequencePointKind_Normal, 0, 148 } /* seqPointIndex: 148 */,
	{ 52115, 3, 63, 63, 13, 53, 1, kSequencePointKind_Normal, 0, 149 } /* seqPointIndex: 149 */,
	{ 52115, 3, 63, 63, 13, 53, 2, kSequencePointKind_StepOut, 0, 150 } /* seqPointIndex: 150 */,
	{ 52115, 3, 63, 63, 13, 53, 9, kSequencePointKind_StepOut, 0, 151 } /* seqPointIndex: 151 */,
	{ 52115, 3, 63, 63, 13, 53, 14, kSequencePointKind_StepOut, 0, 152 } /* seqPointIndex: 152 */,
	{ 52115, 3, 64, 64, 9, 10, 22, kSequencePointKind_Normal, 0, 153 } /* seqPointIndex: 153 */,
	{ 52116, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 154 } /* seqPointIndex: 154 */,
	{ 52116, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 155 } /* seqPointIndex: 155 */,
	{ 52116, 4, 177, 177, 9, 10, 0, kSequencePointKind_Normal, 0, 156 } /* seqPointIndex: 156 */,
	{ 52116, 4, 178, 178, 13, 47, 1, kSequencePointKind_Normal, 0, 157 } /* seqPointIndex: 157 */,
	{ 52116, 4, 178, 178, 0, 0, 11, kSequencePointKind_Normal, 0, 158 } /* seqPointIndex: 158 */,
	{ 52116, 4, 180, 180, 17, 69, 14, kSequencePointKind_Normal, 0, 159 } /* seqPointIndex: 159 */,
	{ 52116, 4, 180, 180, 17, 69, 22, kSequencePointKind_StepOut, 0, 160 } /* seqPointIndex: 160 */,
	{ 52116, 4, 181, 181, 9, 10, 28, kSequencePointKind_Normal, 0, 161 } /* seqPointIndex: 161 */,
	{ 52117, 0, 0, 0, 0, 0, -1, kSequencePointKind_Normal, 0, 162 } /* seqPointIndex: 162 */,
	{ 52117, 0, 0, 0, 0, 0, 16777215, kSequencePointKind_Normal, 0, 163 } /* seqPointIndex: 163 */,
	{ 52117, 4, 185, 185, 9, 10, 0, kSequencePointKind_Normal, 0, 164 } /* seqPointIndex: 164 */,
	{ 52117, 4, 186, 186, 13, 46, 1, kSequencePointKind_Normal, 0, 165 } /* seqPointIndex: 165 */,
	{ 52117, 4, 186, 186, 0, 0, 11, kSequencePointKind_Normal, 0, 166 } /* seqPointIndex: 166 */,
	{ 52117, 4, 187, 187, 17, 75, 14, kSequencePointKind_Normal, 0, 167 } /* seqPointIndex: 167 */,
	{ 52117, 4, 187, 187, 17, 75, 22, kSequencePointKind_StepOut, 0, 168 } /* seqPointIndex: 168 */,
	{ 52117, 4, 188, 188, 9, 10, 28, kSequencePointKind_Normal, 0, 169 } /* seqPointIndex: 169 */,
};
#else
extern Il2CppSequencePoint g_sequencePointsUnityEngine_AudioModule[];
Il2CppSequencePoint g_sequencePointsUnityEngine_AudioModule[1] = { { 0, 0, 0, 0, 0, 0, 0, kSequencePointKind_Normal, 0, 0, } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppCatchPoint g_catchPoints[1] = { { 0, 0, 0, 0, } };
#else
static const Il2CppCatchPoint g_catchPoints[1] = { { 0, 0, 0, 0, } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppSequencePointSourceFile g_sequencePointSourceFiles[] = {
{ "", { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }, //0 
{ "\\Users\\bokken\\buildslave\\unity\\build\\Modules\\Audio\\Public\\ScriptBindings\\Audio.bindings.cs", { 219, 184, 64, 33, 63, 81, 46, 116, 140, 90, 100, 149, 50, 64, 86, 180} }, //1 
{ "\\Users\\bokken\\buildslave\\unity\\build\\Modules\\Audio\\Public\\ScriptBindings\\AudioClipPlayable.bindings.cs", { 136, 110, 11, 239, 4, 37, 180, 165, 136, 112, 116, 151, 134, 78, 48, 235} }, //2 
{ "\\Users\\bokken\\buildslave\\unity\\build\\Modules\\Audio\\Public\\ScriptBindings\\AudioMixerPlayable.bindings.cs", { 61, 101, 161, 191, 246, 243, 230, 247, 173, 244, 46, 184, 65, 58, 4, 90} }, //3 
{ "\\Users\\bokken\\buildslave\\unity\\build\\Modules\\Audio\\Public\\ScriptBindings\\AudioSampleProvider.bindings.cs", { 47, 120, 50, 45, 60, 26, 245, 52, 137, 63, 13, 94, 178, 230, 94, 160} }, //4 
};
#else
static const Il2CppSequencePointSourceFile g_sequencePointSourceFiles[1] = { NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppTypeSourceFilePair g_typeSourceFiles[7] = 
{
	{ 7265, 1 },
	{ 7264, 1 },
	{ 7268, 1 },
	{ 7271, 1 },
	{ 7272, 2 },
	{ 7275, 3 },
	{ 7279, 4 },
};
#else
static const Il2CppTypeSourceFilePair g_typeSourceFiles[1] = { { 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodScope g_methodScopes[13] = 
{
	{ 0, 26 },
	{ 0, 11 },
	{ 0, 84 },
	{ 0, 28 },
	{ 0, 28 },
	{ 0, 12 },
	{ 0, 36 },
	{ 0, 12 },
	{ 0, 24 },
	{ 0, 12 },
	{ 0, 24 },
	{ 0, 29 },
	{ 0, 29 },
};
#else
static const Il2CppMethodScope g_methodScopes[1] = { { 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodHeaderInfo g_methodHeaderInfos[48] = 
{
	{ 26, 0, 1 } /* System.Void UnityEngine.AudioSettings::InvokeOnAudioConfigurationChanged(System.Boolean) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings::InvokeOnAudioSystemShuttingDown() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings::InvokeOnAudioSystemStartedUp() */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.AudioSettings::StartAudioOutput() */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.AudioSettings::StopAudioOutput() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings/AudioConfigurationChangeHandler::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings/AudioConfigurationChangeHandler::Invoke(System.Boolean) */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.AudioSettings/Mobile::get_muteState() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings/Mobile::set_muteState(System.Boolean) */,
	{ 11, 1, 1 } /* System.Boolean UnityEngine.AudioSettings/Mobile::get_stopAudioOutputOnMute() */,
	{ 84, 2, 1 } /* System.Void UnityEngine.AudioSettings/Mobile::InvokeOnMuteStateChanged(System.Boolean) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings/Mobile::StartAudioOutput() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSettings/Mobile::StopAudioOutput() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioClip::.ctor() */,
	{ 28, 3, 1 } /* System.Void UnityEngine.AudioClip::InvokePCMReaderCallback_Internal(System.Single[]) */,
	{ 28, 4, 1 } /* System.Void UnityEngine.AudioClip::InvokePCMSetPositionCallback_Internal(System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioClip/PCMReaderCallback::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioClip/PCMReaderCallback::Invoke(System.Single[]) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioClip/PCMSetPositionCallback::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioClip/PCMSetPositionCallback::Invoke(System.Int32) */,
	{ 0, 0, 0 } /* System.Single UnityEngine.AudioListener::get_volume() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioListener::set_volume(System.Single) */,
	{ 0, 0, 0 } /* System.Single UnityEngine.AudioSource::GetPitch(UnityEngine.AudioSource) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::SetPitch(UnityEngine.AudioSource,System.Single) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::PlayHelper(UnityEngine.AudioSource,System.UInt64) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::PlayOneShotHelper(UnityEngine.AudioSource,UnityEngine.AudioClip,System.Single) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::Stop(System.Boolean) */,
	{ 0, 0, 0 } /* System.Single UnityEngine.AudioSource::get_volume() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::set_volume(System.Single) */,
	{ 12, 5, 1 } /* System.Single UnityEngine.AudioSource::get_pitch() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::set_pitch(System.Single) */,
	{ 0, 0, 0 } /* UnityEngine.AudioClip UnityEngine.AudioSource::get_clip() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::set_clip(UnityEngine.AudioClip) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::Play() */,
	{ 36, 6, 1 } /* System.Void UnityEngine.AudioSource::PlayOneShot(UnityEngine.AudioClip,System.Single) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::Stop() */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.AudioSource::get_isPlaying() */,
	{ 0, 0, 0 } /* System.Void UnityEngine.AudioSource::set_loop(System.Boolean) */,
	{ 12, 7, 1 } /* UnityEngine.Playables.PlayableHandle UnityEngine.Audio.AudioClipPlayable::GetHandle() */,
	{ 24, 8, 1 } /* System.Boolean UnityEngine.Audio.AudioClipPlayable::Equals(UnityEngine.Audio.AudioClipPlayable) */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.Audio.AudioMixer::SetFloat(System.String,System.Single) */,
	{ 0, 0, 0 } /* System.Boolean UnityEngine.Audio.AudioMixer::GetFloat(System.String,System.Single&) */,
	{ 12, 9, 1 } /* UnityEngine.Playables.PlayableHandle UnityEngine.Audio.AudioMixerPlayable::GetHandle() */,
	{ 24, 10, 1 } /* System.Boolean UnityEngine.Audio.AudioMixerPlayable::Equals(UnityEngine.Audio.AudioMixerPlayable) */,
	{ 29, 11, 1 } /* System.Void UnityEngine.Experimental.Audio.AudioSampleProvider::InvokeSampleFramesAvailable(System.Int32) */,
	{ 29, 12, 1 } /* System.Void UnityEngine.Experimental.Audio.AudioSampleProvider::InvokeSampleFramesOverflow(System.Int32) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.Experimental.Audio.AudioSampleProvider/SampleFramesHandler::.ctor(System.Object,System.IntPtr) */,
	{ 0, 0, 0 } /* System.Void UnityEngine.Experimental.Audio.AudioSampleProvider/SampleFramesHandler::Invoke(UnityEngine.Experimental.Audio.AudioSampleProvider,System.UInt32) */,
};
#else
static const Il2CppMethodHeaderInfo g_methodHeaderInfos[1] = { { 0, 0, 0 } };
#endif
IL2CPP_EXTERN_C const Il2CppDebuggerMetadataRegistration g_DebuggerMetadataRegistrationUnityEngine_AudioModule;
const Il2CppDebuggerMetadataRegistration g_DebuggerMetadataRegistrationUnityEngine_AudioModule = 
{
	(Il2CppMethodExecutionContextInfo*)g_methodExecutionContextInfos,
	(Il2CppMethodExecutionContextInfoIndex*)g_methodExecutionContextInfoIndexes,
	(Il2CppMethodScope*)g_methodScopes,
	(Il2CppMethodHeaderInfo*)g_methodHeaderInfos,
	(Il2CppSequencePointSourceFile*)g_sequencePointSourceFiles,
	170,
	(Il2CppSequencePoint*)g_sequencePointsUnityEngine_AudioModule,
	0,
	(Il2CppCatchPoint*)g_catchPoints,
	7,
	(Il2CppTypeSourceFilePair*)g_typeSourceFiles,
	(const char**)g_methodExecutionContextInfoStrings,
};
