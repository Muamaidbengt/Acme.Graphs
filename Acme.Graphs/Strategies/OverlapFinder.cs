namespace Acme.Graphs.Strategies {
    public static class OverlapFinder {
        public static Path FindOverlap(Path first, Path second) {
            ArgumentHelpers.ThrowIfNull(() => first);
            ArgumentHelpers.ThrowIfNull(() => second);
            
            for (var i = 0; i < first.Length; i++) {
                for (var j = 0; j < second.Length; j++) {
                    if (first[i+j] == second[j]) {
                        // yay
                        if (i+j == first.Length - 1) {
                            return Path.Of(first, second.GetRange(j + 1, second.Length - j - 1));
                        }
                    } else { break; }
                }
            }

            return Path.Empty;
        }
    }
}
