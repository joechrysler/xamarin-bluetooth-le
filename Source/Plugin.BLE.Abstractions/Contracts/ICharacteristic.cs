using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;

namespace Plugin.BLE.Abstractions.Contracts
{
    /// <summary>
    /// A bluetooth LE GATT characteristic.
    /// </summary>
    public interface ICharacteristic
    {
        /// <summary>
        /// Event gets raised, when the davice notifies a value change on this characteristic.
        /// To start listening, call <see cref="StartUpdates"/>.
        /// </summary>
        event EventHandler<CharacteristicUpdatedEventArgs> ValueUpdated;

        /// <summary>
        /// Id of the characteristic.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// TODO: review: do we need this in any case?
        /// Uuid of the characteristic.
        /// </summary>
        string Uuid { get; }

        /// <summary>
        /// Name of the charakteristic.
        /// Returns the name if the <see cref="Id"/> is a id of a standard characteristic.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the last known value of the characteristic.
        /// </summary>
        byte[] Value { get; }

        /// <summary>
        /// Gets <see cref="Value"/> as UTF8 encoded string representation.
        /// </summary>
        string StringValue { get; }

        /// <summary>
        /// List of descriptors.
        /// </summary>
        IList<IDescriptor> Descriptors { get; }

        /// <summary>
        /// Properties of the characteristic.
        /// </summary>
        CharacteristicPropertyType Properties { get; }

        /// <summary>
        /// Indicates wheter the characteristic can be read or not.
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// Indicates wheter the characteristic can be written or not.
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// Indicates wheter the characteristic supports notify or not.
        /// </summary>
        bool CanUpdate { get; }

        /// <summary>
        /// Reads the characteristic value from the device. The result is also stored inisde the Value property.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The Result property will contain the read bytes.</returns>
        /// <exception cref="InvalidOperationException">Thrown if characteristic doesn't support read. See: <see cref="CanRead"/></exception>
        /// <exception cref="CharacteristicReadException">Thrown if the reading of the value failed.</exception>
        Task<byte[]> ReadAsync();

        /// <summary>
        /// Sends <paramref name="data"/> as characteristic value to the device.
        /// </summary>
        /// <param name="data">Data that should be written.</param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The Task will finish after the value was written. The Result property will be <c>true</c> if the value
        /// was written successful, otherwise <c>false</c>.
        /// If the characteristic is write with response, the Task will finish if the value has been written. 
        /// If it is write without response, the task will immediately finish with <c>true</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if characteristic doesn't support write. See: <see cref="CanWrite"/></exception>
        /// <exception cref="ArgumentNullException">Thrwon if <paramref name="data"/> is null.</exception>
        Task<bool> WriteAsync(byte[] data);

        /// <summary>
        /// Starts listening for notify events on this characteristic.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if characteristic doesn't support notify. See: <see cref="CanUpdate"/></exception>
        void StartUpdates();

        /// <summary>
        /// Stops listening for notify events on this characteristic.
        /// </summary>
        void StopUpdates();
    }
}

