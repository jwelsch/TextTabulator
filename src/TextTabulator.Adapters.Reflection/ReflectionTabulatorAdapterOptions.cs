namespace TextTabulator.Adapters.Reflection
{
    /// <summary>
    /// Options to allow configuration of the ReflectionTabulatorAdapter class.
    /// </summary>
    public class ReflectionTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to type member names.
        /// </summary>
        public INameTransform MemberNameTransform { get; }

        /// <summary>
        /// Gets which type members to include in the output.
        /// </summary>
        public TypeMembers TypeMembers { get; }

        /// <summary>
        /// Gets the desired access modifier(s) of the type members to include in the output.
        /// </summary>
        public AccessModifiers AccessModifiers { get; }

        /// <summary>
        /// Creates an object of type ReflectionTabulatorAdapterOptions.
        /// </summary>
        /// <param name="memberNameTransform">Transform to apply to type member names. Passing null will cause the member names to not be altered.</param>
        /// <param name="typeMembers">Specifies which type members to include in the output.</param>
        /// <param name="accessModifiers">Specifies the desired access modifier(s) of the type members to include in the output.</param>
        public ReflectionTabulatorAdapterOptions(INameTransform? memberNameTransform = null, TypeMembers typeMembers = TypeMembers.Properties, AccessModifiers accessModifiers = AccessModifiers.Public)
        {
            MemberNameTransform = memberNameTransform ?? new PassThruNameTransform();
            TypeMembers = typeMembers;
            AccessModifiers = accessModifiers;
        }
    }
}
