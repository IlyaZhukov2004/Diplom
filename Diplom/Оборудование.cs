//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diplom
{
    using System;
    using System.Collections.Generic;
    
    public partial class Оборудование
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Оборудование()
        {
            this.Заявка = new HashSet<Заявка>();
        }
    
        public int ID { get; set; }
        public string Индивидуальный_номер { get; set; }
        public int ID_вид_оборудования { get; set; }
        public string Описание { get; set; }
    
        public virtual Вид_оборудования Вид_оборудования { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заявка> Заявка { get; set; }
    }
}
