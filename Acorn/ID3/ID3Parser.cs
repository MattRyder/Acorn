using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Acorn.iTunes;

namespace Acorn.ID3
{
    public class ID3Parser
    {
        /// <summary>
        /// Parses the ID3 Header Frames from an MP3 
        /// </summary>
        /// <returns></returns>
        public Song parseHeader(Stream fileStream)
        {
            int idHeaderSize;
            Song song = new Song();
            UTF8Encoding encoder = new UTF8Encoding();

            byte[] idHeader = new byte[10];
            fileStream.Read(idHeader, 0, 10);

            //Parse to String and compare with expected magic:
            if (encoder.GetString(idHeader).Substring(0, 3).Equals("ID3"))
            {
                byte[] idSize = new byte[4];
                Array.Copy(idHeader, 6, idSize, 0, 4);

                idHeaderSize = ReadSynchsafeInt32(idSize, 0)+10;

                while (fileStream.Position < idHeaderSize)
                {
                    //Store all frame data:
                    byte[] frameData = new byte[10];
                    fileStream.Read(frameData, 0, 10);

                    //Get the name:
                    byte[] bFrameID = new byte[4];
                    Array.Copy(frameData, 0, bFrameID, 0, bFrameID.Length);
                    string frameID = encoder.GetString(bFrameID);

                    byte[] bFrameSize = new byte[4];
                    Array.Copy(frameData, 4, bFrameSize, 0, bFrameSize.Length);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bFrameSize);

                    //Convert raw bytes to a uint for a size:
                    uint frameSize = BitConverter.ToUInt32(bFrameSize, 0);

                    byte[] bFrameContent = new byte[frameSize];
                    fileStream.Read(bFrameContent, 0, bFrameContent.Length);

                    //Now just check if we're caching it, and convert to relevent Object:
                    if (frameID.Equals("COMM"))
                    {
                        song.setAttribute("Comments", encoder.GetString(bFrameContent).Trim('\0'));
                    }
                    else if (frameID.Equals("TALB"))
                    {
                        song.setAttribute("Album", encoder.GetString(bFrameContent).Trim('\0'));
                    }
                    else if (frameID.Equals("TPE1") || frameID.Equals("TPE2"))
                    {
                        song.setAttribute("Artist", encoder.GetString(bFrameContent).Trim('\0'));
                    }
                    else if (frameID.Equals("TCON"))
                    {
                        var genre = ID3.Genres[BitConverter.ToInt32(bFrameContent, 0)];
                    }



                    Console.WriteLine("{0} - {1} bytes", frameID, frameSize);
                }
            }
            return song;
        }

        //Used under licence from: walkmen.codeplex.com
        private int ReadSynchsafeInt32(byte[] buffer, int offset)
        {
            return ((buffer[offset + 0] & 0x7F) << 21) | ((buffer[offset + 1] & 0x7F) << 14) | 
                    ((buffer[offset + 2] & 0x7F) << 7) | (buffer[offset + 3] & 0x7f);
        }   
    }
}
