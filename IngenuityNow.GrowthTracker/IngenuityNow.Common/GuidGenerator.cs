using System;
using System.Runtime.InteropServices;

namespace IngenuityNow.Common
{
    /// <summary>
    /// Generator for new GUIDs.
    /// </summary>
    public static class GuidGenerator
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        /// <summary>
        /// Generate a new <see cref="Guid"/>, optionally generating it sequentially and shuffling bytes to match SQL server specs.
        /// </summary>
        /// <param name="sequential">Whether to generate a sequential <see cref="Guid"/>.</param>
        /// <param name="sqlServerShuffle">Whether to apply the same shuffling as SQL server applies.</param>
        /// <returns>A new <see cref="Guid"/>.</returns>
        public static Guid NewId(bool sequential = true, bool sqlServerShuffle = true)
        {
            if (!sequential)
                return Guid.NewGuid();

            UuidCreateSequential(out Guid guid);

            if (!sqlServerShuffle)
                return guid;

            var s = guid.ToByteArray();
            var t = new byte[16];
            t[3] = s[0];
            t[2] = s[1];
            t[1] = s[2];
            t[0] = s[3];
            t[5] = s[4];
            t[4] = s[5];
            t[7] = s[6];
            t[6] = s[7];
            t[8] = s[8];
            t[9] = s[9];
            t[10] = s[10];
            t[11] = s[11];
            t[12] = s[12];
            t[13] = s[13];
            t[14] = s[14];
            t[15] = s[15];

            return new Guid(t);
        }
    }
}
