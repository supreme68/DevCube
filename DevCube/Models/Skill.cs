//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevCube.Models {
   using System;
   using System.Collections.Generic;

   public partial class Skill {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      public Skill()
      {
      this.Programmers_Skills = new HashSet<Programmers_Skills>();
      }

      public int SkillID { get; set; }
      public string Name { get; set; }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<Programmers_Skills> Programmers_Skills { get; set; }
   }
}
