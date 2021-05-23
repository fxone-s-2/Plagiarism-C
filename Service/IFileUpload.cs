using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plagiarism_C.Service
{
   public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
    }
}
