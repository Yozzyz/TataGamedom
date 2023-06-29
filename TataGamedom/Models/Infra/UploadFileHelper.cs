using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Infra
{
	public class UploadFileHelper
	{
		private readonly IRenameProvider _renameProvider;
		private readonly IUploadValidator[] _validators;

		public UploadFileHelper(IRenameProvider renameProvider, params IUploadValidator[] validators)
		{
			_renameProvider = renameProvider;
			_validators = validators;
		}

		public string SaveFile(HttpPostedFileBase file, string path)
		{
			if (file == null || file.ContentLength == 0) return null;

			if (!_validators.All(v => v.IsValid(file))) return null;

			var newFileName = _renameProvider.GetNewFileName(path, file.FileName);

			var fullName = Path.Combine(path, newFileName);
			file.SaveAs(fullName);

			return newFileName;
		}
	}

	#region RenameProviders
	public interface IRenameProvider
	{
		string GetNewFileName(string path, string fileName);
	}

	public class GuidRenameProvider : IRenameProvider
	{
		public string GetNewFileName(string path, string fileName)
		{
			var extension = Path.GetExtension(fileName);

			return $"{Guid.NewGuid().ToString("N")}{extension}";
		}
	}
	#endregion

	#region UploadValidators
	public interface IUploadValidator
	{
		bool IsValid(HttpPostedFileBase file);
	}

	public class RequiredValidator : IUploadValidator
	{
		public bool IsValid(HttpPostedFileBase file)
		{
			return file != null && file.ContentLength > 0;
		}
	}

	public class ImageValidator : IUploadValidator
	{
		public bool IsValid(HttpPostedFileBase file)
		{
			if (file == null || file.ContentLength == 0) return true;

			var extension = Path.GetExtension(file.FileName);

			return IsImageFile(extension);
		}

		private bool IsImageFile(string extension)
		{
			var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

			return imageExtensions.Any(e => e.Equals(extension, StringComparison.OrdinalIgnoreCase));
		}
	}

	public class FileSizeValidator : IUploadValidator
	{
		private readonly int _maxSize;

		public FileSizeValidator(int maxSize)
		{
			_maxSize = maxSize;
		}

		public bool IsValid(HttpPostedFileBase file)
		{
			if (file == null || file.ContentLength == 0) return true;

			return file.ContentLength <= _maxSize;
		}
	}
	#endregion
}