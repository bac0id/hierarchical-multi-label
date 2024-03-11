using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HML {
	public class Label {

		private SortedSet<Label> children = new SortedSet<Label>();

		public Label(string text) {
			this.Text = text;
		}

		public Label(string text, Label parent) {
			this.Text = text;
			SetParent(parent);
		}

		public string Text { get; set; }

		public string Description { get; set; }

		public Label Parent { get; private set; }

		public IReadOnlyCollection<Label> Children { get => this.children; }

		public void AddChild(Label child) {
			if (child == null) {
				throw new ArgumentNullException(nameof(child));
			}
			if (child.Parent != null) {
				child.RemoveFromParent();
			}
			child.Parent = this;
			this.children.Add(child);
		}

		public void SetParent(Label parent) {
			if (parent == null) {
				throw new ArgumentNullException(nameof(parent));
			}
			if (this.Parent != null) {
				this.RemoveFromParent();
			}
			this.Parent = parent;
			parent.children.Add(this);
		}

		private void RemoveFromParent() {
			this.Parent.children.Remove(this);
			this.Parent = null;
		}
	}
}
