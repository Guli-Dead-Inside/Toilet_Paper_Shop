//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Toilet_Paper_Shop.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            this.MaterialsAndProducts = new HashSet<MaterialsAndProducts>();
            this.Product = new HashSet<Product>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public int id_Type { get; set; }
        public Nullable<int> CountPackaged { get; set; }
        public string Unit { get; set; }
        public Nullable<int> CountStocked { get; set; }
        public Nullable<int> MinimumBalance { get; set; }
        public Nullable<decimal> Cost { get; set; }
    
        public virtual TypeMaterial TypeMaterial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialsAndProducts> MaterialsAndProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
