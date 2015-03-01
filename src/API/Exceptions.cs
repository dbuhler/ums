using System;


namespace API
{
    /// <summary>
    /// Thrown when the specified birth date or death date exists in the future 
    /// or the birth date or death date exists before the earliest allowable date (currently 1900).
    /// </summary>
    public class InvalidDateException : Exception
    {
    }

    /// <summary>
    /// Thrown when the specified email does not contain the structure
    /// of a valid email address or contains illegal characters.
    /// </summary>
    public class InvalidEmailException : Exception
    {
    }

    /// <summary>
    /// Thrown when the specified record does not exist in the database.
    /// </summary>
    public class RecordNotFoundException : Exception
    {
    }

    /// <summary>
    /// Thrown when the specified name contains illegal characters (ie. numbers).
    /// </summary>
    public class InvalidNameException : Exception
    {
    }

    /// <summary>
    /// Thrown when the record to be added already exists within the database.
    /// </summary>
    public class DuplicateRecordException : Exception
    {
    }

    /// <summary>
    /// Thrown when the specified Canadian SIN does not validate according to the official equation.
    /// </summary>
    public class InvalidSinException : Exception
    {
    }
}
